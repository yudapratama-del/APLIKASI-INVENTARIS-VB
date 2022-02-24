Public Class frmMasterRuangPeminjaman
    Dim Tampung As New DataTable
    Dim StatusDataBaru As Boolean

    Sub TampilTabel()
        Tampung = ExecuteQuery("select * from ruang_peminjaman")
        GridControl1.DataSource = Tampung
        ExecuteGridViewAllAppearance(GridView1)
    End Sub

    Sub Databaru()
        StatusDataBaru = True
        ExecuteInputTextValueClear(txtNamaRuang, txtKeterangan)
        txtKodeRuang.Text = ExecuteAutoCode("ruang_peminjaman", "KodeRuang", "RPM", "000000")
        TampilTabel()


    End Sub

    Sub Simpan()
        If txtKodeRuang.Text = "" Or txtNamaRuang.Text = "" Or txtKeterangan.Text = "" Then
            MessageBox.Show("Kode Ruang, Nama Ruang dan Keterangan belum Diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If StatusDataBaru = True Then
            Dim Cari = Tampung.Select("KodeRuang='" & txtKodeRuang.Text & "'")
            If Cari.Length <= 0 Then
                'INSERT
                ExecuteQuery("INSERT INTO ruang_peminjaman(KodeRuang,NamaRuang,Keterangan) VALUES ('" & txtKodeRuang.Text & "','" & txtNamaRuang.Text & "','" & txtKeterangan.Text & "')")
                Databaru()
                MessageBox.Show("Data Berhasil Disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data Gagal Disimpan!" & vbCrLf & "Data Sudah Ada Di Database!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            'UPDATE
            ExecuteQuery("UPDATE ruang_peminjaman SET NamaRuang='" & txtNamaRuang.Text & "', Keterangan='" & txtKeterangan.Text & "' WHERE KodeRuang='" & txtKodeRuang.Text & "'")
            Databaru()
            MessageBox.Show("Data Berhasil Disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Sub Ubah()
        If GridView1.RowCount > 0 Then
            StatusDataBaru = False
            txtKodeRuang.Text = GridView1.GetFocusedRowCellValue("KodeRuang")
            txtNamaRuang.Text = GridView1.GetFocusedRowCellValue("NamaRuang")
            txtKeterangan.Text = GridView1.GetFocusedRowCellValue("Keterangan")
            txtNamaRuang.Focus()

        End If
    End Sub

    Sub Hapus()
        If GridView1.RowCount > 0 Then
            If MessageBox.Show("Data Akan Dihapus! Lanjutkan?", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'DELETE
                ExecuteQuery("DELETE FROM ruang_peminjaman WHERE KodeRuang='" & GridView1.GetFocusedRowCellValue("KodeRuang") & "'")
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

    Private Sub frmMasterRuangPeminjaman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtKodeRuang.ReadOnly = True
        txtKodeRuang.TabStop = False
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