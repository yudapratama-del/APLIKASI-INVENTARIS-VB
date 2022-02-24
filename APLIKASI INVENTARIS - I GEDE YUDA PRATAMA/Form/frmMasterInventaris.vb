Public Class frmMasterInventaris 
    Dim Tampung As New DataTable
    Dim StatusDataBaru As Boolean

    Sub TampilTabel()
        GridControl1.DataSource = Nothing
        GridView1.Columns.Clear()
        Tampung = ExecuteQuery("select a.KodeInventaris, a.Nama,a.Kondisi,a.Keterangan,a.Jumlah,'" & Format(txtTanggal.DateTime, "yyyy-MM-dd HH:mm:ss") & "' as TanggalRegister,a.KodeJenis,a.KodeRuang,a.KodePengguna,b.NamaJenis,c.NamaRuang,d.NamaPengguna,e.NIP,e.NamaPegawai from (inventaris a inner join jenis b on a.KodeJenis=b.KodeJenis inner join ruang_penyimpanan c on a.KodeRuang=c.KodeRuang inner join pengguna d on a.KodePengguna=d.KodePengguna inner join pegawai e on a.NIP=e.NIP)")
        GridControl1.DataSource = Tampung
        ExecuteGridViewAllAppearance(GridView1, , "KodeJenis,KodeRuang,KodePengguna,NIP", "Jumlah", , "Jumlah")
    End Sub

    Sub Databaru()
        StatusDataBaru = True
        ExecuteInputTextValueClear(txtNamaInventaris, txtKet, txtKodeJns, txtKodeRng, txtKondisi, txtNIP)
        txtTanggal.DateTime = Now
        txtJumlah.Value = 0
        txtkodeInventaris.Text = ExecuteAutoCode("inventaris", "KodeInventaris", "INV", "000000")
        TampilTabel()
        txtNamaInventaris.Focus()

    End Sub

    Sub Simpan()
        If txtkodeInventaris.Text = "" Or txtNamaInventaris.Text = "" Or txtJumlah.Value = 0 Or txtKet.Text = "" Or txtKodeJns.Text = "" Or txtKodePengguna.Text = "" Or txtKodeRng.Text = "" Or txtKodeRng.Text = "" Or txtKondisi.Text = "" Then
            MessageBox.Show("Kode Inventaris, Nama Inventaris, Jumlah, Kondisi, Kode Jenis, Kode ruang, Kode Pengguna, NIP, Tanggal Register dan Keterangan Wajib Diisi!!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If StatusDataBaru = True Then
            Dim Cari = Tampung.Select("KodeInventaris='" & txtkodeInventaris.Text & "'")
            If Cari.Length <= 0 Then
                'INSERT
                ExecuteQuery("INSERT INTO inventaris (KodeInventaris, Nama, Kondisi, Keterangan, Jumlah, TanggalRegister, KodeJenis, KodeRuang, KodePengguna, NIP) VALUES ('" & txtkodeInventaris.Text & "','" & txtNamaInventaris.Text & "','" & txtKondisi.Text & "','" & txtKet.Text & "','" & txtJumlah.Value & "','" & Format(txtTanggal.DateTime, "yyyy-MM-dd HH:mm:ss") & "','" & txtKodeJns.Text & "','" & txtKodeRng.Text & "','" & txtKodePengguna.Text & "','" & txtNIP.Text & "')")
                Databaru()
                MessageBox.Show("DataBerhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data Gagal Disimpan!" & vbCrLf & "Data Sudah Ada Dalam Database", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            'UPDATE
            ExecuteQuery("UPDATE inventaris SET Nama='" & txtNamaInventaris.Text & "',Kondisi='" & txtKondisi.Text & "',Keterangan='" & txtKet.Text & "',Jumlah='" & txtJumlah.Value & "',TanggalRegister='" & Format(txtTanggal.DateTime, "yyyy-MM-dd HH:mm:ss") & "',KodeJenis='" & txtKodeJns.Text & "',KodeRuang='" & txtKodeRng.Text & "',KodePengguna='" & txtKodePengguna.Text & "',NIP='" & txtNIP.Text & "' WHERE KodeInventaris='" & txtkodeInventaris.Text & "'")
            Databaru()
            MessageBox.Show("Data Berhasil Disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Sub Ubah()
        If GridView1.RowCount > 0 Then
            StatusDataBaru = False
            txtkodeInventaris.Text = GridView1.GetFocusedRowCellValue("KodeInventaris")
            txtNamaInventaris.Text = GridView1.GetFocusedRowCellValue("Nama")
            txtKondisi.Text = GridView1.GetFocusedRowCellValue("Kondisi")
            txtKet.Text = GridView1.GetFocusedRowCellValue("Keterangan")
            txtJumlah.Text = GridView1.GetFocusedRowCellValue("Jumlah")
            txtTanggal.Text = GridView1.GetFocusedRowCellValue("TanggalRegister")
            txtKodeJns.Text = GridView1.GetFocusedRowCellValue("KodeJenis")
            txtKodeRng.Text = GridView1.GetFocusedRowCellValue("KodeRuang")
            txtNIP.Text = GridView1.GetFocusedRowCellValue("NIP")
            txtNamaInventaris.Focus()
        Else
            MessageBox.Show("Tidak ada Data yang Dipilih!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Hapus()
        If GridView1.RowCount > 0 Then
            If MessageBox.Show("Data Akan Dihapus! Lanjutkan?", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'DELETE
                ExecuteQuery("DELETE FROM inventaris WHERE KodeInventaris='" & GridView1.GetFocusedRowCellValue("KodeInventaris") & "'")
                Databaru()
            End If
        Else
            MessageBox.Show("Tidak Ada Data Yang dipilih!","Kesalahan",MessageBoxButtons.OK,MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Cetak()
        GridControl1.ShowRibbonPrintPreview()
    End Sub

    Sub Tutup()
        Me.Close()

    End Sub

    Sub TampilRuangPenyimpanan()
        frmListRuangPenyimpanan.NilaiKiriman = 1
        frmListRuangPenyimpanan.ShowDialog()

    End Sub

    Sub TampilJenis()
        frmListJenisInventaris.NilaiKiriman = 1
        frmListJenisInventaris.ShowDialog()

    End Sub

    Sub TampilPegawai()
        frmListPegawai.NilaiKiriman = 1
        frmListPegawai.ShowDialog()

    End Sub

    Private Sub frmMasterInventaris_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case e.Control And Keys.Enter
                Simpan()
            Case Keys.F5
                Databaru()
            Case Keys.Escape
                Me.Close()
            Case Keys.F2
                Ubah()
            Case e.Control And Keys.Delete
                Hapus()
            Case Keys.F8
                Cetak()
        End Select
    End Sub

    Private Sub frmMasterInventaris_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtkodeInventaris.ReadOnly = True
        txtkodeInventaris.TabStop = False
        txtkodeInventaris.ReadOnly = True
        txtKodeJns.ReadOnly = True
        txtKodeRng.ReadOnly = True
        txtNIP.ReadOnly = True
        txtKodePengguna.ReadOnly = True
        txtKodePengguna.Text = My.Settings.lgnKodePengguna
        Databaru()

    End Sub

    Private Sub txtKondisi_GotFocus(sender As Object, e As EventArgs) Handles txtKondisi.GotFocus
        ExecuteComboBoxList("inventaris", "Kondisi", txtKondisi)
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Simpan()

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Databaru()

    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Tutup()

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

    Private Sub txtKodeRng_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeRng.ButtonClick
        TampilRuangPenyimpanan()
    End Sub

    Private Sub txtKodeJns_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeJns.ButtonClick
        TampilJenis()
    End Sub

    Private Sub txtNIP_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtNIP.ButtonClick
        TampilPegawai()

    End Sub
End Class