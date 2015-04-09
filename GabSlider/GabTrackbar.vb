Imports System.Windows.Forms
Imports System.Threading
Imports System.Drawing

Public Class GabTrackbar
#Region " Variables "

    Private _Bitonio As Bitonio
    Private _TrackSize As Integer
    Private _TrackBackgroundColor As Color
    Private _TrackBorderColor As Color
    Private _TrackBorderPen As Pen
    Private _TrackBorderSize As Integer
    Private _TrackWithGradient As Boolean
    Private _TrackGradientColor1 As Color
    Private _TrackGradientColor2 As Color
    Private _TrackSolidBrush As SolidBrush
    Private _TrackGradientBrush As Drawing2D.LinearGradientBrush
    Private _BorderColor As Color
    Private _BorderSize As Integer
    Private _BorderStyle As ButtonBorderStyle
    Private _Minimum As Integer
    Private _Maximum As Integer
    Private _Value As Integer
    Private _IncrementSmall As UInteger
    Private _IncrementBig As UInteger

    Private _IsLoaded As Boolean
    Private _BitonioClicked As Boolean
    Private Shared _Position_Souris_X As Integer
    Private Shared _Position_Souris_Y As Integer
    Private _Orientation As eOrientation
    Private _bg As BufferedGraphics
    Private _bgc As New BufferedGraphicsContext()

    Public Shadows Event Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
    Public Event ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event MinimumChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event MaximumChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event MinimumReached(ByVal sender As Object, ByVal e As EventArgs)
    Public Event MaximumReached(ByVal sender As Object, ByVal e As EventArgs)
    Public Event TrackClick(ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event TrackHover(ByVal sender As Object, ByVal e As EventArgs)
    Public Event BitonioClick(ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event BitonioHover(ByVal sender As Object, ByVal e As EventArgs)
    Public Event PaintFinished(ByVal sender As Object, ByVal e As PaintEventArgs)


#End Region

#Region " Constructor "

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'spécifie que l'on dessine soit-même
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        UpdateStyles()

        _IsLoaded = False

        AutoSize = False
        _Maximum = 100
        _Minimum = 0
        _Value = 0
        _IncrementSmall = 1
        _IncrementBig = 10
        Width = 500
        Height = 100
        _BorderColor = Color.Black
        _BorderSize = 1
        _Orientation = eOrientation.Horizontal

        MyBase.BorderStyle = System.Windows.Forms.BorderStyle.None
        BorderStyle = ButtonBorderStyle.Solid


        _TrackSize = 20
        _TrackBackgroundColor = Color.Red
        _TrackSolidBrush = New SolidBrush(_TrackBackgroundColor)
        _TrackBorderColor = Color.Black
        _TrackBorderSize = 1
        _TrackBorderPen = New Pen(_TrackBorderColor, _TrackBorderSize)
        _TrackWithGradient = False
        _TrackGradientColor1 = Color.Yellow
        _TrackGradientColor2 = Color.Red
        _TrackGradientBrush = New Drawing2D.LinearGradientBrush(New Point(_BorderSize, 0), New Point(Me.Width - _BorderSize, 0), _TrackGradientColor1, _TrackGradientColor2)
        _Bitonio = New Bitonio(Me)
        _BitonioClicked = False

    End Sub
#End Region

#Region " Enums "

    Public Enum eOrientation
        Horizontal = 0
        Vertical = 1
    End Enum

#End Region

#Region " Properties "
    Public Property Bitonio() As Bitonio
        Get
            Return _Bitonio
        End Get
        Set(ByVal value As Bitonio)
            _Bitonio = value
            Refresh()
        End Set
    End Property

    Public ReadOnly Property BitonioSizeWithBorders() As Size
        Get
            Return _Bitonio.BitonioSizeWithBorders
        End Get
    End Property
    Public Property BitonioSizeWithoutBorders() As Size
        Get
            Return _Bitonio.BitonioSizeWithoutBorders
        End Get
        Set(ByVal value As Size)
            _Bitonio.BitonioSizeWithoutBorders = value
        End Set
    End Property
    Public Property BitonioBorderStyle() As Bitonio.eBorderStyle
        Get
            Return _Bitonio.BitonioBorderStyle
        End Get
        Set(ByVal value As Bitonio.eBorderStyle)
            _Bitonio.BitonioBorderStyle = value
        End Set
    End Property
    Public Property BitonioBorderSize() As Integer
        Get
            Return _Bitonio.BitonioBorderSize
        End Get
        Set(ByVal value As Integer)
            _Bitonio.BitonioBorderSize = value

        End Set
    End Property
    Public Property BitonioBorderColor() As Color
        Get
            Return _Bitonio.BitonioBorderColor
        End Get
        Set(ByVal value As Color)
            _Bitonio.BitonioBorderColor = value

        End Set
    End Property
    Public Property BitonioBackColor() As Color
        Get
            Return _Bitonio.BitonioBackColor
        End Get
        Set(ByVal value As Color)
            _Bitonio.BitonioBackColor = value
        End Set
    End Property
    Public Property BitonioType() As Bitonio.eBitonioType
        Get
            Return _Bitonio.BitonioType
        End Get
        Set(ByVal value As Bitonio.eBitonioType)
            _Bitonio.BitonioType = value

        End Set
    End Property
    Public Property BitonioFill() As Bitonio.eBitonioFill
        Get
            Return _Bitonio.BitonioFill
        End Get
        Set(ByVal value As Bitonio.eBitonioFill)
            _Bitonio.BitonioFill = value
        End Set
    End Property
    Public Property BitonioCustomShape() As Drawing2D.GraphicsPath
        Get
            Return _Bitonio.BitonioCustomShape
        End Get
        Set(ByVal value As Drawing2D.GraphicsPath)
            _Bitonio.BitonioCustomShape = value
        End Set
    End Property
    Public Property BitonioImage() As Bitmap
        Get
            Return _Bitonio.BitonioImage
        End Get
        Set(ByVal value As Bitmap)
            _Bitonio.BitonioImage = value
        End Set
    End Property
    Public Property BitonioBackTextureWrapMode As Drawing2D.WrapMode
        Get
            Return _Bitonio.BitonioBackTextureWrapMode
        End Get
        Set(ByVal value As Drawing2D.WrapMode)
            _Bitonio.BitonioBackTextureWrapMode = value
        End Set
    End Property
    Public Property Minimum() As Integer
        Get
            Return _Minimum
        End Get
        Set(ByVal value As Integer)
            If value < _Maximum And value <= Me.Value Then
                _Minimum = value
                Refresh()
                RaiseEvent MinimumChanged(Me, New EventArgs())
            End If
        End Set
    End Property
    Public Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal value As Integer)
            If value > _Minimum And value >= Me.Value Then
                _Maximum = value
                Refresh()
                RaiseEvent MaximumChanged(Me, New EventArgs())
            End If
        End Set
    End Property
    Public Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)

            If value <> _Value Then
                If value >= _Minimum Then
                    If value <= _Maximum Then
                        _Value = value
                    Else
                        _Value = Maximum
                    End If
                Else
                    _Value = Minimum
                End If
                Refresh()
                RaiseEvent ValueChanged(Me, New EventArgs())
            End If

        End Set
    End Property

    Public Property SmallIncrement() As UInteger
        Get
            Return _IncrementSmall
        End Get
        Set(ByVal value As UInteger)
            If value <= _IncrementBig Then
                _IncrementSmall = value
            End If
        End Set
    End Property

    Public Property BigIncrement() As UInteger
        Get
            Return _IncrementBig
        End Get
        Set(ByVal value As UInteger)
            If value >= _IncrementSmall Then
                _IncrementBig = value
            End If
        End Set
    End Property

    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
            Refresh()
        End Set
    End Property
    Public Property BorderSize() As Integer
        Get
            Return _BorderSize
        End Get
        Set(ByVal value As Integer)
            _BorderSize = value
            Refresh()
        End Set
    End Property

    Public Shadows Property BorderStyle() As ButtonBorderStyle
        Get
            Return _BorderStyle
        End Get
        Set(ByVal value As ButtonBorderStyle)
            _BorderStyle = value
            Refresh()
        End Set
    End Property

    Public Property TrackSize() As Integer
        Get
            Return _TrackSize
        End Get
        Set(ByVal value As Integer)
            _TrackSize = value
            Refresh()
        End Set
    End Property
    Public Property TrackBackgroundColor As Color
        Get
            Return _TrackBackgroundColor
        End Get
        Set(ByVal value As Color)
            _TrackBackgroundColor = value
            _TrackSolidBrush.Color = _TrackBackgroundColor
            Refresh()
        End Set
    End Property
    Public Property TrackBorderColor As Color
        Get
            Return _TrackBorderColor
        End Get
        Set(ByVal value As Color)
            _TrackBorderColor = value
            _TrackBorderPen.Color = _TrackBorderColor
            Refresh()
        End Set
    End Property
    Public Property TrackBorderSize As Integer
        Get
            Return _TrackBorderSize
        End Get
        Set(ByVal value As Integer)
            _TrackBorderSize = value
            _TrackBorderPen.Width = _TrackBorderSize
            Refresh()
        End Set
    End Property
    Public Property TrackWithGradient As Boolean
        Get
            Return _TrackWithGradient
        End Get
        Set(ByVal value As Boolean)
            _TrackWithGradient = value
            Refresh()
        End Set
    End Property
    Public Property TrackGradientColor1 As Color
        Get
            Return _TrackGradientColor1
        End Get
        Set(ByVal value As Color)
            _TrackGradientColor1 = value
            _TrackGradientBrush.LinearColors(0) = value
            Refresh()
        End Set
    End Property
    Public Property TrackGradientColor2 As Color
        Get
            Return _TrackGradientColor2
        End Get
        Set(ByVal value As Color)
            _TrackGradientColor2 = value
            _TrackGradientBrush.LinearColors(1) = value
            Refresh()
        End Set
    End Property
    Public Property Orientation() As eOrientation
        Get
            Return _Orientation
        End Get
        Set(ByVal value As eOrientation)
            _Orientation = value
            Refresh()
        End Set
    End Property

#End Region

#Region " Private methods "

    Private Sub GabTrackbar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _IsLoaded = True
    End Sub

    Private Sub GabTrackbar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If _Bitonio.IsClicked(e.Location) = True Then
                _BitonioClicked = True
                RaiseEvent BitonioClick(sender, e)
            Else
                _BitonioClicked = False

                If IsTrackClicked(e.Location) Then
                    RaiseEvent TrackClick(sender, e)
                End If

            End If
        End If
    End Sub

    Private Sub GabTrackbar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseHover
        If _Bitonio.IsClicked(PointToClient(Cursor.Position)) = True Then
            RaiseEvent BitonioHover(sender, e)
        Else
            If IsTrackClicked(PointToClient(Cursor.Position)) Then
                RaiseEvent TrackHover(sender, e)
            End If
        End If
    End Sub

    Private Sub GabTrackbar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        _BitonioClicked = False
    End Sub

    Private Sub GabTrackbar_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

        If _BitonioClicked = True Then

            'We must move the bitonio to one extremity if the mouse is over the bounds
            '(necessary or the bitonio will sometimes stop before its min or max, which is irritating)
            'So we compute the difference between the cursor position and the control bounds
            Dim pt1 As Point = System.Windows.Forms.Cursor.Position

            If (pt1.X <> _Position_Souris_X And _Orientation = eOrientation.Horizontal) Or (pt1.Y <> _Position_Souris_Y And _Orientation = eOrientation.Vertical) Then
                Dim pt2 As Point = Point.Subtract(PointToScreen(Me.Location), New Size(Me.Location))
                If _Orientation = eOrientation.Horizontal Then

                    If pt1.X < pt2.X Then
                        Me.Value = _Minimum
                    ElseIf pt1.X > pt2.X + Me.Bounds.Width Then
                        Me.Value = _Maximum
                    Else
                        Me.Value = CInt(((e.Location.X - _Bitonio.BitonioSizeWithBorders.Width / 2) * _Maximum) / (Me.Width - _Bitonio.BitonioSizeWithBorders.Width))
                    End If

                Else

                    If pt1.Y > pt2.Y + Me.Bounds.Height Then
                        Me.Value = _Minimum
                    ElseIf pt1.Y < pt2.Y Then
                        Me.Value = _Maximum
                    Else
                        Me.Value = CInt(_Maximum - (((e.Location.Y - _Bitonio.BitonioSizeWithBorders.Height / 2) * _Maximum) / (Me.Height - _Bitonio.BitonioSizeWithBorders.Height)))
                    End If


                End If

                RaiseEvent Scroll(sender, New System.Windows.Forms.ScrollEventArgs(ScrollEventType.ThumbTrack, _Value))

                _Position_Souris_X = pt1.X
                _Position_Souris_Y = pt1.Y

                If _Value = _Minimum Then
                    RaiseEvent MinimumReached(sender, New EventArgs())
                ElseIf _Value = _Maximum Then
                    RaiseEvent MaximumReached(sender, New EventArgs())
                End If

            End If

        End If


    End Sub

    Private Sub GabTrackbar_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        _BitonioClicked = False
    End Sub

    Private Sub GabTrackbar_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If e.Delta < 0 Then
            Me.Value = CInt(Me.Value - _IncrementBig)
        Else
            Me.Value = CInt(Me.Value + _IncrementBig)
        End If
    End Sub

    Private Sub GabTrackbar_TrackClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.TrackClick

        If _Orientation = eOrientation.Horizontal Then
            Me.Value = CInt(((e.Location.X - _Bitonio.BitonioSizeWithBorders.Width / 2) * _Maximum) / (Me.Width - _Bitonio.BitonioSizeWithBorders.Width))
        Else
            Me.Value = CInt(_Maximum - (((e.Location.Y - _Bitonio.BitonioSizeWithBorders.Height / 2) * _Maximum) / (Me.Height - _Bitonio.BitonioSizeWithBorders.Height)))
        End If

    End Sub

    Private Sub GabTrackBar_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Not _IsLoaded Then Exit Sub

        PaintAll(e)
    End Sub

    ''' <summary>
    ''' We paint everything here
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PaintAll(ByRef e As System.Windows.Forms.PaintEventArgs)
        Dim x1 As Integer
        Dim x2 As Integer
        Dim y1 As Integer
        Dim y2 As Integer

        Dim MyWidth As Integer = Me.Width
        Dim MyHeight As Integer = Me.Height


        'allocate a Graphics buffer
        _bg = _bgc.Allocate(e.Graphics, New Rectangle(0, 0, Me.Bounds.Width, Me.Bounds.Height))

        If _Bitonio.BitonioType <> Bitonio.eBitonioType.Ellipse Then
            _bg.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed 'so that the rectangle or picture are not antialiased
        Else
            _bg.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality 'so that the ellipse shape looks pretty, but the borders of the control can't be dotted/dashed (ugly)
        End If

        _bg.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixel
        _bg.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighSpeed
        _bg.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighSpeed
        _bg.Graphics.InterpolationMode = Drawing2D.InterpolationMode.Low

        'paint the background
        _bg.Graphics.Clear(Me.BackColor)

        'paint the border
        If _BorderSize > 0 Then
            ControlPaint.DrawBorder(_bg.Graphics, New Rectangle(0, 0, MyWidth, MyHeight), _
                                    _BorderColor, _BorderSize, _BorderStyle, _
                                    _BorderColor, _BorderSize, _BorderStyle, _
                                    _BorderColor, _BorderSize, _BorderStyle, _
                                    _BorderColor, _BorderSize, _BorderStyle)

        End If

        'paint the track
        If _Orientation = eOrientation.Horizontal Then

            x1 = _BorderSize
            y1 = MyHeight \ 2 - _TrackSize \ 2 - _TrackBorderSize \ 2
            x2 = MyWidth - _BorderSize
            y2 = MyHeight \ 2 + _TrackSize \ 2 + _TrackBorderSize \ 2

            If _TrackWithGradient Then
                'TODO : find a way to reuse the LinearGradientBrush (must change its ReadOnly Rectangle)
                _TrackGradientBrush.Dispose()
                _TrackGradientBrush = New Drawing2D.LinearGradientBrush(New Point(x1 - 1, 0), New Point(x2, 0), _TrackGradientColor1, _TrackGradientColor2)
                _bg.Graphics.FillRectangle(_TrackGradientBrush, New Rectangle(x1, MyHeight \ 2 - _TrackSize \ 2, MyWidth - (_BorderSize * 2), _TrackSize))
            Else
                _bg.Graphics.FillRectangle(_TrackSolidBrush, New Rectangle(x1, MyHeight \ 2 - _TrackSize \ 2, MyWidth - (_BorderSize * 2), _TrackSize))
            End If

            If _TrackBorderSize > 0 Then
                _bg.Graphics.DrawLine(_TrackBorderPen, x1, y1, DirectCast(IIf(_TrackBorderSize > 1, x2, x2 - 1), Integer), y1)
                _bg.Graphics.DrawLine(_TrackBorderPen, x1, y2, DirectCast(IIf(_TrackBorderSize > 1, x2, x2 - 1), Integer), y2)
            End If

        Else
            x1 = MyWidth \ 2 - _TrackSize \ 2 - _TrackBorderSize \ 2
            y1 = _BorderSize
            x2 = MyWidth \ 2 + _TrackSize \ 2 + _TrackBorderSize \ 2
            y2 = MyHeight - _BorderSize

            If _TrackWithGradient Then
                'TODO : find a way to reuse the LinearGradientBrush (must change its ReadOnly Rectangle)
                _TrackGradientBrush.Dispose()
                _TrackGradientBrush = New Drawing2D.LinearGradientBrush(New Point(0, y2), New Point(0, y1 - 1), _TrackGradientColor1, _TrackGradientColor2)
                _bg.Graphics.FillRectangle(_TrackGradientBrush, New Rectangle(MyWidth \ 2 - _TrackSize \ 2, y1, _TrackSize, MyHeight - (_BorderSize * 2)))
            Else
                _bg.Graphics.FillRectangle(_TrackSolidBrush, New Rectangle(MyWidth \ 2 - _TrackSize \ 2, y1, _TrackSize, MyHeight - (Me.BorderSize * 2)))
            End If

            If TrackBorderSize > 0 Then
                _bg.Graphics.DrawLine(_TrackBorderPen, x1, y1, x1, DirectCast(IIf(_TrackBorderSize > 1, y2, y2 - 1), Integer))
                _bg.Graphics.DrawLine(_TrackBorderPen, x2, y1, x2, DirectCast(IIf(_TrackBorderSize > 1, y2, y2 - 1), Integer))
            End If


        End If

        'paint the bitonio
        Dim min As Integer
        Dim max As Integer
        Dim offsetx As Integer
        Dim offsety As Integer

        If _Orientation = eOrientation.Horizontal Then
            min = _BorderSize
            max = MyWidth - _Bitonio.BitonioSizeWithBorders.Width - (_BorderSize * 2)
            offsetx = CInt(((_Value * max) / _Maximum) + min)
            offsety = MyHeight \ 2 - _Bitonio.BitonioSizeWithBorders.Height \ 2
        Else
            min = _BorderSize
            max = MyHeight - _Bitonio.BitonioSizeWithBorders.Height - (_BorderSize * 2)
            offsetx = MyWidth \ 2 - _Bitonio.BitonioSizeWithBorders.Width \ 2
            offsety = CInt(max - (((_Value * max) / _Maximum)) + min)
        End If

        _Bitonio.PaintBitonio(_bg, offsetx, offsety)

        _bg.Render()
        _bg.Dispose()

        RaiseEvent PaintFinished(Me, e)

    End Sub

    Private Function IsTrackClicked(ByVal pt As Point) As Boolean
        Dim x1 As Integer
        Dim y1 As Integer
        Dim x2 As Integer
        Dim y2 As Integer

        If Me.Orientation = eOrientation.Horizontal Then
            x1 = _BorderSize
            y1 = Me.Height \ 2 - _TrackSize \ 2 - TrackBorderSize \ 2
            x2 = Me.Width - _BorderSize
            y2 = Me.Height \ 2 + _TrackSize \ 2 + TrackBorderSize \ 2
        Else
            x1 = Me.Width \ 2 - TrackSize \ 2 - TrackBorderSize \ 2
            y1 = _BorderSize
            x2 = Me.Width \ 2 + _TrackSize \ 2 + TrackBorderSize \ 2
            y2 = Me.Height - _BorderSize
        End If

        Return pt.X >= x1 And pt.X <= x2 And pt.Y >= y1 And pt.Y <= y2

    End Function

#End Region

#Region " Public methods "
    Public Delegate Sub RefreshCallback()
    Public Overrides Sub Refresh()
        If Me.InvokeRequired Then
            Dim d As New RefreshCallback(AddressOf Refresh)
            Me.Invoke(d)
        Else

            MyBase.Refresh()
        End If
    End Sub


#End Region


End Class
