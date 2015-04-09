<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim GraphicsPath1 As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.GabTrackbar1 = New GabTrackbar.GabTrackbar()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(308, 269)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(308, 293)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(308, 318)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Label3"
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropertyGrid1.Location = New System.Drawing.Point(725, 12)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(338, 460)
        Me.PropertyGrid1.TabIndex = 5
        '
        'GabTrackbar1
        '
        Me.GabTrackbar1.BigIncrement = CType(10UI, UInteger)
        Me.GabTrackbar1.BitonioBackColor = System.Drawing.Color.CornflowerBlue
        Me.GabTrackbar1.BitonioBackTextureWrapMode = System.Drawing.Drawing2D.WrapMode.Clamp
        Me.GabTrackbar1.BitonioBorderColor = System.Drawing.Color.Black
        Me.GabTrackbar1.BitonioBorderSize = 2
        Me.GabTrackbar1.BitonioBorderStyle = GabTrackbar.Bitonio.eBorderStyle.FixedSingle
        GraphicsPath1.FillMode = System.Drawing.Drawing2D.FillMode.Alternate
        Me.GabTrackbar1.BitonioCustomShape = GraphicsPath1
        Me.GabTrackbar1.BitonioFill = GabTrackbar.Bitonio.eBitonioFill.Solid
        Me.GabTrackbar1.BitonioImage = CType(resources.GetObject("GabTrackbar1.BitonioImage"), System.Drawing.Image)
        Me.GabTrackbar1.BitonioSizeWithoutBorders = New System.Drawing.Size(32, 32)
        Me.GabTrackbar1.BitonioType = GabTrackbar.Bitonio.eBitonioType.Rectangle
        Me.GabTrackbar1.BorderColor = System.Drawing.Color.Black
        Me.GabTrackbar1.BorderSize = 1.0!
        Me.GabTrackbar1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid
        Me.GabTrackbar1.Location = New System.Drawing.Point(81, 37)
        Me.GabTrackbar1.Maximum = 255
        Me.GabTrackbar1.Minimum = 0
        Me.GabTrackbar1.Name = "GabTrackbar1"
        Me.GabTrackbar1.Orientation = GabTrackbar.GabTrackbar.eOrientation.Horizontal
        Me.GabTrackbar1.Size = New System.Drawing.Size(500, 100)
        Me.GabTrackbar1.SmallIncrement = CType(1UI, UInteger)
        Me.GabTrackbar1.TabIndex = 0
        Me.GabTrackbar1.TrackBackgroundColor = System.Drawing.Color.Red
        Me.GabTrackbar1.TrackBorderColor = System.Drawing.Color.Black
        Me.GabTrackbar1.TrackBorderSize = 1
        Me.GabTrackbar1.TrackGradientColor1 = System.Drawing.Color.Yellow
        Me.GabTrackbar1.TrackGradientColor2 = System.Drawing.Color.Red
        Me.GabTrackbar1.TrackSize = 10
        Me.GabTrackbar1.TrackWithGradient = True
        Me.GabTrackbar1.Value = 50
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1075, 484)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GabTrackbar1)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GabTrackbar1 As GabTrackbar.GabTrackbar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid

End Class
