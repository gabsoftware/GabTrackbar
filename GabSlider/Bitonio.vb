Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

''' <summary>
''' A bitonio is a thing. Here the bitonio represent the TrackBar slider position
''' </summary>
''' <remarks>Un vrai petit bitonio !</remarks>
Public Class Bitonio

#Region " Variables "
    Private _BitonioSizeWithBorders As Size
    Private _BitonioSizeWithoutBorders As Size
    Private _BitonioBorderStyle As eBorderStyle
    Private _BitonioBorderSize As Integer
    Private _BitonioType As eBitonioType
    Private _BitonioFill As eBitonioFill
    Private _BitonioBackColor As Color
    Private _BitonioBorderColor As Color
    Private _BitonioCustomShape As GraphicsPath
    Private _BitonioImage As Bitmap
    Private _BitonioIsLoaded As Boolean
    Private _BitonioOffsetX As Integer
    Private _BitonioOffsetY As Integer
    Private _BitonioBackSolidBrush As SolidBrush
    Private _BitonioBackTextureBrush As TextureBrush
    Private _BitonioBackTextureWrapMode As WrapMode
    Private _BitonioBorderPen As Pen
    Private _BitonioParent As GabTrackbar
#End Region

#Region " Constructor "
    Public Sub New(ByRef myParent As GabTrackbar)
        _BitonioIsLoaded = False
        _BitonioSizeWithoutBorders = New Size(30, 30)
        _BitonioSizeWithBorders = New Size(30 + 2 * _BitonioBorderSize, 30 + 2 * _BitonioBorderSize)
        _BitonioType = eBitonioType.Rectangle
        _BitonioFill = eBitonioFill.Solid
        _BitonioBackColor = Color.CornflowerBlue
        _BitonioBorderColor = Color.Black
        _BitonioBorderStyle = eBorderStyle.FixedSingle
        _BitonioBorderSize = 2
        _BitonioOffsetX = 0
        _BitonioOffsetY = 0
        _BitonioParent = myParent
        _BitonioIsLoaded = True
        _BitonioBackSolidBrush = New SolidBrush(_BitonioBackColor)
        _BitonioBackTextureWrapMode = WrapMode.Clamp
        _BitonioBorderPen = New Pen(_BitonioBorderColor)
    End Sub
#End Region

#Region " Enums "
    Public Enum eBorderStyle As Integer
        None = 0
        FixedSingle = 1
    End Enum
    Public Enum eBitonioType As Integer
        Rectangle = 0
        Ellipse = 1
        Image = 2
        CustomShape = 4
    End Enum

    Public Enum eBitonioFill As Integer
        None = 0
        Solid = 1
        Texture = 2
    End Enum


#End Region

#Region " Properties "

    Public Property BitonioParent() As GabTrackbar
        Get
            Return _BitonioParent
        End Get
        Set(ByVal value As GabTrackbar)
            _BitonioParent = value
            NeedRefresh()
        End Set
    End Property

    Public Property BitonioSizeWithoutBorders As Size
        Get
            Return _BitonioSizeWithoutBorders
        End Get
        Set(ByVal value As Size)
            _BitonioSizeWithoutBorders = value
            _BitonioSizeWithBorders = New Size(value.Width + _BitonioBorderSize * 2, value.Height + _BitonioBorderSize * 2)
            NeedRefresh()
        End Set
    End Property
    Public ReadOnly Property BitonioSizeWithBorders As Size
        Get
            Return _BitonioSizeWithBorders
        End Get
    End Property
    Public Property BitonioOffsetX() As Integer
        Get
            Return _BitonioOffsetX
        End Get
        Set(ByVal value As Integer)
            _BitonioOffsetX = value
            NeedRefresh()
        End Set
    End Property
    Public Property BitonioOffsetY() As Integer
        Get
            Return _BitonioOffsetY
        End Get
        Set(ByVal value As Integer)
            _BitonioOffsetY = value
            NeedRefresh()
        End Set
    End Property
    Public Property BitonioBorderStyle() As eBorderStyle
        Get
            Return _BitonioBorderStyle
        End Get
        Set(ByVal value As eBorderStyle)
            _BitonioBorderStyle = value
            NeedRefresh()
        End Set
    End Property
    Public Property BitonioBorderSize() As Integer
        Get
            Return _BitonioBorderSize
        End Get
        Set(ByVal value As Integer)
            _BitonioBorderSize = value
            _BitonioBorderPen.Width = value

            _BitonioSizeWithBorders = New Size(_BitonioSizeWithoutBorders.Width + 2 * value, _BitonioSizeWithoutBorders.Height + 2 * value)

            NeedRefresh()
        End Set
    End Property
    Public Property BitonioBorderColor As Color
        Get
            Return _BitonioBorderColor
        End Get
        Set(ByVal value As Color)
            _BitonioBorderColor = value
            _BitonioBorderPen.Color = value
            NeedRefresh()
        End Set
    End Property
    Public Property BitonioBackColor As Color
        Get
            Return _BitonioBackColor
        End Get
        Set(ByVal value As Color)
            _BitonioBackColor = value
            _BitonioBackSolidBrush.Color = value
            NeedRefresh()
        End Set
    End Property
    Public Property BitonioType() As eBitonioType
        Get
            Return _BitonioType
        End Get
        Set(ByVal value As eBitonioType)
            _BitonioType = value
            NeedRefresh()
        End Set
    End Property
    Public Property BitonioFill() As eBitonioFill
        Get
            Return _BitonioFill
        End Get
        Set(ByVal value As eBitonioFill)
            _BitonioFill = value
            NeedRefresh()
        End Set
    End Property
    Public Property BitonioCustomShape() As GraphicsPath
        Get
            Return _BitonioCustomShape
        End Get
        Set(ByVal value As GraphicsPath)
            _BitonioCustomShape = value
            NeedRefresh()
        End Set
    End Property
    Public Property BitonioImage() As Bitmap
        Get
            Return _BitonioImage
        End Get
        Set(ByVal value As Bitmap)
            If _BitonioImage IsNot Nothing Then
                _BitonioImage.Dispose()
            End If
            _BitonioImage = value
            If _BitonioBackTextureBrush IsNot Nothing Then
                _BitonioBackTextureBrush.Dispose()
            End If
            _BitonioBackTextureBrush = New TextureBrush(value, _BitonioBackTextureWrapMode)
            NeedRefresh()
        End Set
    End Property

    Public Property BitonioBackTextureWrapMode As WrapMode
        Get
            Return _BitonioBackTextureWrapMode
        End Get
        Set(ByVal value As WrapMode)
            _BitonioBackTextureWrapMode = value
            If _BitonioBackTextureBrush IsNot Nothing Then
                _BitonioBackTextureBrush.WrapMode = value
            End If
            NeedRefresh()
        End Set
    End Property

#End Region

#Region " Private methods "
    'Private Function OrBits(ByVal number As Integer, ByVal searched As Integer)
    '    Return IIf((number Or searched) = number, True, False)
    'End Function
#End Region

#Region " Public methods "
    Public Sub PaintBitonio(ByRef _bg As BufferedGraphics, ByVal myOffsetX As Integer, ByVal myOffsetY As Integer)

        'met à jour sans provoquer de nouveaux raffraichissements
        _BitonioOffsetX = myOffsetX
        _BitonioOffsetY = myOffsetY

        If _BitonioType = eBitonioType.Ellipse Then
            If _BitonioBorderStyle = eBorderStyle.FixedSingle Then
                _bg.Graphics.DrawEllipse(_BitonioBorderPen, _BitonioOffsetX + _BitonioBorderSize \ 2, _BitonioOffsetY + _BitonioBorderSize \ 2, _BitonioSizeWithBorders.Width - _BitonioBorderSize, _BitonioSizeWithBorders.Height - _BitonioBorderSize)
            End If
            If _BitonioFill = eBitonioFill.Solid Then
                _bg.Graphics.FillEllipse(_BitonioBackSolidBrush, _BitonioOffsetX + _BitonioBorderSize, _BitonioOffsetY + _BitonioBorderSize, _BitonioSizeWithoutBorders.Width, _BitonioSizeWithoutBorders.Height)


            End If

        Else
            If _BitonioType = eBitonioType.Rectangle Then
                If _BitonioBorderStyle = eBorderStyle.FixedSingle Then
                    _bg.Graphics.DrawRectangle(_BitonioBorderPen, _BitonioOffsetX + _BitonioBorderSize \ 2, _BitonioOffsetY + _BitonioBorderSize \ 2, _BitonioSizeWithBorders.Width - _BitonioBorderSize, _BitonioSizeWithBorders.Height - _BitonioBorderSize)
                End If
                If _BitonioFill = eBitonioFill.Solid Then
                    _bg.Graphics.FillRectangle(_BitonioBackSolidBrush, _BitonioOffsetX + _BitonioBorderSize, _BitonioOffsetY + _BitonioBorderSize, _BitonioSizeWithoutBorders.Width, _BitonioSizeWithoutBorders.Height)
                ElseIf _BitonioFill = eBitonioFill.Texture And _BitonioBackTextureBrush IsNot Nothing Then
                    _bg.Graphics.FillRectangle(_BitonioBackTextureBrush, BitonioOffsetX + _BitonioBorderSize, _BitonioOffsetY + _BitonioBorderSize, _BitonioSizeWithoutBorders.Width, _BitonioSizeWithoutBorders.Height)
                End If

            Else
                If _BitonioType = eBitonioType.Image Then
                    If _BitonioImage Is Nothing Then
                        _BitonioImage = My.Resources.DefaultImage
                    End If
                    _bg.Graphics.DrawImage(_BitonioImage, _BitonioOffsetX, _BitonioOffsetY, _BitonioSizeWithBorders.Width, _BitonioSizeWithBorders.Height)
                Else
                    If _BitonioType = eBitonioType.CustomShape Then
                        If _BitonioCustomShape Is Nothing Then
                            _BitonioCustomShape = New GraphicsPath()
                            _BitonioCustomShape.AddEllipse(_BitonioOffsetX, _BitonioOffsetY, _BitonioSizeWithBorders.Width, _BitonioSizeWithBorders.Height)
                        End If
                        _bg.Graphics.FillPath(_BitonioBackSolidBrush, _BitonioCustomShape)
                        If _BitonioBorderStyle = eBorderStyle.FixedSingle Then
                            _bg.Graphics.DrawPath(_BitonioBorderPen, _BitonioCustomShape)
                        End If
                    Else
                        'l'utilisateur n'a rien compris et/ou a saisi une mauvaise valeur
                        If _BitonioBorderStyle = eBorderStyle.FixedSingle Then
                            _bg.Graphics.DrawRectangle(_BitonioBorderPen, _BitonioOffsetX + _BitonioBorderSize \ 2, _BitonioOffsetY + _BitonioBorderSize \ 2, _BitonioSizeWithBorders.Width - _BitonioBorderSize, _BitonioSizeWithBorders.Height - _BitonioBorderSize)
                        End If
                        If _BitonioFill = eBitonioFill.Solid Then
                            _bg.Graphics.FillRectangle(_BitonioBackSolidBrush, _BitonioOffsetX + _BitonioBorderSize, _BitonioOffsetY + _BitonioBorderSize, _BitonioSizeWithoutBorders.Width, _BitonioSizeWithoutBorders.Height)
                        ElseIf _BitonioFill = eBitonioFill.Texture And _BitonioBackTextureBrush IsNot Nothing Then
                            _bg.Graphics.FillRectangle(_BitonioBackTextureBrush, BitonioOffsetX + _BitonioBorderSize, _BitonioOffsetY + _BitonioBorderSize, _BitonioSizeWithoutBorders.Width, _BitonioSizeWithoutBorders.Height)
                        End If
                    End If
                End If
            End If
        End If


    End Sub
    Public Sub NeedRefresh()
        If Not _BitonioIsLoaded Then
            Exit Sub
        End If

        _BitonioParent.Refresh()
    End Sub

    Public Function IsClicked(ByVal location As Point) As Boolean
        Return _BitonioOffsetX <= location.X And location.X <= _BitonioOffsetX + _BitonioSizeWithBorders.Width _
            And _BitonioOffsetY <= location.Y And location.Y <= _BitonioOffsetY + _BitonioSizeWithBorders.Height
    End Function

#End Region


End Class
