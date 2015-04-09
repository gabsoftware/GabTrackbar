Public Class Form1

    Private counter As Int64 = 0

    Private Sub GabTrackbar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GabTrackbar1.MouseMove, GabTrackbar1.MouseWheel
        Label1.Text = GabTrackbar1.Value
        Label2.Text = GabTrackbar1.Bitonio.BitonioOffsetX
    End Sub

    ' When the mouse hovers over Button2, its ClientRectangle is filled.
    Private Sub Button2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim senderControl As Control = Me 'CType(sender, Control)
        Dim screenRectangle As Rectangle = senderControl.RectangleToScreen(senderControl.ClientRectangle)
        ControlPaint.FillReversibleRectangle(screenRectangle, senderControl.BackColor)
    End Sub


    ' When the mouse leaves Button2, its ClientRectangle is cleared by
    ' calling the FillReversibleRectangle method again.
    Private Sub Button2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim senderControl As Control = Me 'CType(sender, Control)
        Dim screenRectangle As Rectangle = senderControl.RectangleToScreen(senderControl.ClientRectangle)
        ControlPaint.FillReversibleRectangle(screenRectangle, senderControl.BackColor)
    End Sub


    Private Sub GabTrackbar1_PaintFinished(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles GabTrackbar1.PaintFinished
        counter += 1
        Label3.Text = counter
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PropertyGrid1.SelectedObject = GabTrackbar1
    End Sub
End Class
