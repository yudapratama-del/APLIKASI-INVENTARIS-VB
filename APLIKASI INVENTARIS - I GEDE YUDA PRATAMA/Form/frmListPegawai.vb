Public Class frmListPegawai
    Dim Tampung As New DataTable
    Public NilaiKiriman As Integer

    Sub TampilTabel()
        Tampung = ExecuteQuery("select * from pegawai")
        GridControl1.DataSource = Tampung
        ExecuteGridViewAllAppearance(GridView1)
    End Sub

    Sub Pilih()
        If GridView1.RowCount > 0 And GridView1.GetFocusedRowCellValue("NIP") <> "" Then
            Select Case NilaiKiriman
                Case 1
                    frmMasterInventaris.txtNIP.Text = GridView1.GetFocusedRowCellValue("NIP")
                Case 2
                    frmTransaksiPeminjaman.txtNIP.Text = GridView1.GetFocusedRowCellValue("NIP")
                    frmTransaksiPeminjaman.txtNamaPegawai.Text = GridView1.GetFocusedRowCellValue("NamaPegawai")
                    
                Case 3
                    frmTransaksiPengembalian.txtNIP.Text = GridView1.GetFocusedRowCellValue("NIP")
                    frmTransaksiPengembalian.txtNamaPegawai = GridView1.GetFocusedRowCellValue("NamaPegawai")
            End Select
            Me.Close()
        Else
            MessageBox.Show("Belum ada data yang di pilih!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub frmListPenyimpanan_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case e.Control And Keys.Enter
                Pilih()
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub frmListPenyimpanan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        TampilTabel()
    End Sub

    Private Sub txtPencarian_EditValueChanged(sender As Object, e As EventArgs) Handles txtPencarian.EditValueChanged
        GridView1.FindFilterText = txtPencarian.Text
    End Sub


    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Pilih()

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()

    End Sub
End Class