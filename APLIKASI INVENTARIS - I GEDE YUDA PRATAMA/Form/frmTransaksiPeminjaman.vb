Public Class frmTransaksiPeminjaman 
    Dim Tampung As New DataTable
    Public PilihInventaris As Boolean

    Sub TampilTabel()
        Tampung = ExecuteQuery("select a.KodeInventaris,a.Jumlah,b.Nama,b.Kondisi,b.KodeJenis,b.KodeRuang,b.KodePengguna from peminjaman_detil a, inventaris b where a.KodeInventaris=b.KodeInventaris and a.NomorPeminjaman='000000000000'")
        GridControl1.DataSource = Tampung
        ExecuteGridViewAllAppearance(GridView1)
    End Sub

    Sub Databaru()
        TampilTabel()
        ExecuteInputTextValueClear(txtScanBarcode, txtNoPeminjaman, txtNIP, txtKodeRuang, txtNamaRuang, txtKeteranganPinjam, txtNamaPegawai)
        txtTanggal.DateTime = Now
        txtTanggalPinjam.DateTime = Now
        txtNoPeminjaman.Text = ExecuteAutoCode("peminjaman", "NomorPeminjaman", "PMJ", "000000000000")
        txtScanBarcode.Focus()

    End Sub

    Sub TampilListInventaris()
        PilihInventaris = False
        frmListInventaris.NilaiKiriman = 1
        frmListInventaris.ShowDialog()
        If PilihInventaris = True Then
            TambahItem()

        End If
    End Sub

    Sub TampilListPegawai()
        frmListPegawai.NilaiKiriman = 2
        frmListPegawai.ShowDialog()

    End Sub

    Sub TampilListRuangPeminjaman()
        frmListRuangPeminjaman.NilaiKiriman = 1
        frmListRuangPeminjaman.ShowDialog()

    End Sub

    Sub TambahItemDetil(ByVal KodeInventaris As String, ByVal Nama As String, ByVal Kondisi As String, ByVal Keterangan As String, ByVal Jumlah As Double, ByVal TanggalRegister As Date, ByVal KodeJenis As String, ByVal KodeRuang As String, ByVal KodePengguna As String)
        Dim CariDaftar = Tampung.Select("KodeInventaris='" & KodeInventaris & "'")
        If CariDaftar.Length <= 0 Then
            Dim BarisBaru = Tampung.NewRow
            BarisBaru.Item("KodeInventaris") = KodeInventaris
            BarisBaru.Item("Nama") = Nama
            BarisBaru.Item("Kondisi") = Kondisi
            BarisBaru.Item("Jumlah") = 1
            BarisBaru.Item("KodeJenis") = KodeJenis
            BarisBaru.Item("KodeRuang") = KodeRuang
            BarisBaru.Item("KodePengguna") = KodePengguna
            Tampung.Rows.Add(BarisBaru)
        Else
            CariDaftar(0).Item("Jumlah") = CariDaftar(0).Item("Jumlah") + 1
        End If
    End Sub

    Sub TambahItem()
        Dim CariKodeInventaris = ExecuteQuery("select * from inventaris where KodeInventaris='" & txtScanBarcode.Text & "'").Select()
        If CariKodeInventaris.Length > 0 Then
            TambahItemDetil(CariKodeInventaris(0).Item("KodeInventaris"), CariKodeInventaris(0).Item("Nama"), CariKodeInventaris(0).Item("Kondisi"), CariKodeInventaris(0).Item("Keterangan"), CariKodeInventaris(0).Item("Jumlah"), CariKodeInventaris(0).Item("TanggalRegister"), CariKodeInventaris(0).Item("KodeJenis"), CariKodeInventaris(0).Item("KodeRuang"), CariKodeInventaris(0).Item("KodePengguna"))
            txtScanBarcode.Text = ""
            txtScanBarcode.Focus()
        Else
            MessageBox.Show("Inventaris Tidak Ditemukan.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtScanBarcode.Text = ""
            txtScanBarcode.Focus()

        End If
    End Sub

    Sub HapusItem()
        If GridView1.RowCount > 0 Then
            GridView1.DeleteSelectedRows()
        Else
            MessageBox.Show("Tidak Ada Daftar Yang Dipilih.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Simpan()
        If GridView1.RowCount < 0 Then
            MessageBox.Show("Daftar Item Peminjaman Masih Kosong!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If txtNoPeminjaman.Text = "" Or txtTanggalPinjam.Text = "" Or txtNIP.Text = "" Or txtNamaPegawai.Text = "" Or txtKodeRuang.Text = "" Or txtNamaRuang.Text = "" Or txtKeteranganPinjam.Text = "" Then
            MessageBox.Show("Nomor Peminjaman, Tanggal Pinjam, NIP, Nama Pegawai, Kode Ruang, Nama Ruang dan Keterangan Pinjam, Wajib Diisi!!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim NomorPeminjaman As String = ExecuteAutoCode("peminjaman", "NomorPeminjaman", "PMJ", "000000000000")
        ExecuteQuery("INSERT INTO peminjaman (NomorPeminjaman,Tanggal,NIP,KodeRuang,TanggalPinjam,StatusPinjam,KeteranganPinjam) VALUES ('" & txtNoPeminjaman.Text & "','" & Format(txtTanggal.DateTime, "yyyy-MM-dd HH:mm:ss") & "','" & txtNIP.Text & "','" & txtKodeRuang.Text & "','" & Format(txtTanggalPinjam.DateTime, "yyyy-MM-dd HH:mm:ss") & "','DIPINJAM','" & txtKeteranganPinjam.Text & "')")
        For Each Isi In Tampung.Select()
            ExecuteQuery("INSERT INTO peminjaman_detil(NomorPeminjaman,KodeInventaris,Jumlah) VALUES ('" & NomorPeminjaman & "', '" & Isi.Item("KodeInventaris") & "', " & Isi.Item("Jumlah") & ")")
        Next
        Databaru()
        MessageBox.Show("Transaksi Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Sub Tutup()
        If GridView1.RowCount > 0 Then
            If MessageBox.Show("Daftar Belanja Masih Terisi. Lanjutkan Tutup Transaksi?", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Me.Close()

            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmTransaksiPeminjaman_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case e.Control And Keys.S
                Simpan()
            Case Keys.F5
                Databaru()
            Case Keys.Escape
                Tutup()

        End Select
    End Sub

    Private Sub frmTransaksiPeminjaman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtTanggal.Properties.ReadOnly = True
        txtNoPeminjaman.Properties.ReadOnly = True
        txtNoPeminjaman.TabStop = False
        txtNIP.Properties.ReadOnly = True
        txtNIP.TabStop = False
        txtNamaPegawai.Properties.ReadOnly = True
        txtNamaPegawai.TabStop = False
        txtKodeRuang.Properties.ReadOnly = True
        txtKodeRuang.TabStop = False
        txtNamaRuang.Properties.ReadOnly = True
        txtNamaRuang.TabStop = False
        txtpengguna.Text = "Pengguna : " & My.Settings.lgnNamaPengguna
        Databaru()
    End Sub

    Private Sub txtScanBarcode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtScanBarcode.ButtonClick
        TampilListInventaris()

    End Sub

    Private Sub txtScanBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtScanBarcode.KeyPress
        If e.KeyChar = Chr(13) Then
            TambahItem()

        End If
    End Sub

    Private Sub txtNIP_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtNIP.ButtonClick
        TampilListPegawai()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        HapusItem()

    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Databaru()

    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Simpan()

    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Tutup()

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        Select Case e.Column.GetTextCaption
            Case "Jumlah"
        End Select
        GridView1.RefreshData()
        GridView1.ExpandAllGroups()
        GridView1.BestFitColumns()

    End Sub


    Private Sub btnTambahItem_Click(sender As Object, e As EventArgs) Handles btnTambahItem.Click
        TambahItem()

    End Sub

    Private Sub txtKodeRuang_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeRuang.ButtonClick
        TampilListRuangPeminjaman()

    End Sub
End Class