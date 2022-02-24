Public Class frmMasterJenis 
    Dim Tampung As New DataTable
    Dim StatusDataBaru As Boolean

    Sub TampilTabel()
        Tampung = ExecuteQuery("select * from jenis")
        GridControl1.DataSource = Tampung
        ExecuteGridViewAllAppearance(GridView1)
    End Sub

    Sub Databaru()
        StatusDataBaru = True
        ExecuteInputTextValueClear(txtNamaJenis, txtKeterangan)
        txtKodeJenis.Text = ExecuteAutoCode("jenis", "KodeJenis", "JNS", "000000")
        TampilTabel()
        Me.txtNamaJenis.Focus()

    End Sub

    Sub Simpan()
        If txtKodeJenis.Text = "" Or txtNamaJenis.Text = "" Or txtKeterangan.Text = "" Then
            MessageBox.Show("Kode Jenis, Nama Jenis dan Keterangan belum Diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If StatusDataBaru = True Then
            Dim Cari = Tampung.Select("KodeJenis='" & txtKodeJenis.Text & "'")
            If Cari.Length <= 0 Then
                'INSERT
                ExecuteQuery("INSERT INTO jenis(KodeJenis,NamaJenis,Keterangan) VALUES ('" & txtKodeJenis.Text & "','" & txtNamaJenis.Text & "','" & txtKeterangan.Text & "')")
                Databaru()
                MessageBox.Show("Data Berhasil Disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data Gagal Disimpan!" & vbCrLf & "Data Sudah Ada Di Database!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            'UPDATE
            ExecuteQuery("UPDATE jenis SET NamaJenis='" & txtNamaJenis.Text & "', Keterangan='" & txtKeterangan.Text & "' WHERE KodeJenis='" & txtKodeJenis.Text & "'")
            Databaru()
            MessageBox.Show("Data Berhasil Disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Sub Ubah()
        If GridView1.RowCount > 0 Then
            StatusDataBaru = False
            txtKodeJenis.Text = GridView1.GetFocusedRowCellValue("KodeJenis")
            txtNamaJenis.Text = GridView1.GetFocusedRowCellValue("NamaJenis")
            txtKeterangan.Text = GridView1.GetFocusedRowCellValue("Keterangan")
            txtNamaJenis.Focus()

        End If
    End Sub

    Sub Hapus()
        If GridView1.RowCount > 0 Then
            If MessageBox.Show("Data Akan Dihapus! Lanjutkan?", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'DELETE
                ExecuteQuery("DELETE FROM jenis WHERE KodeJenis='" & GridView1.GetFocusedRowCellValue("KodeJenis") & "'")
                Databaru()

            End If
        Else
            MessageBox.Show("Tidak ada Data yang DIpilih!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Cetak()
        GridControl1.ShowRibbonPrintPreview()

    End Sub

    Sub Tutup()
        Me.Close()

    End Sub

    Private Sub frmMasterJenis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtKodeJenis.ReadOnly = True
        txtKodeJenis.TabStop = False
        txtNamaJenis.Focus()

        Databaru()

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Simpan()

    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Me.Close()

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Databaru()

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Ubah()

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Hapus()

    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Cetak()

    End Sub
End Class