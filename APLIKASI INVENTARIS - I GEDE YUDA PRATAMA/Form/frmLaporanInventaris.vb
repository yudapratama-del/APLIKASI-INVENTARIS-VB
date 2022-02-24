Public Class frmLaporanInventaris 
    Dim Tampung As New DataTable

    Sub TampilTabel()
        GridControl1.DataSource = Nothing
        GridView1.Columns.Clear()
        Select Case txtKategori.SelectedIndex
            Case 0
                Tampung = ExecuteQuery("select a.Tanggal, a.NomorPeminjaman, d.Jumlah, a.NIP, b.NamaPegawai, a.KodeRuang, a.TanggalPinjam, a.StatusPinjam, a.KeteranganPinjam from (peminjaman a inner join pegawai b on a.NIP=b.NIP inner join peminjaman_detil d on a.NomorPeminjaman=d.NomorPeminjaman) where date_format(a.Tanggal, '%Y-%m-%d') between '" & Format(txtTanggalAwal.DateTime, "yyyy-MM-dd") & "' and '" & Format(txtTanggalAkhir.DateTime, "yyyy-MM-dd") & "' and StatusPinjam='DIPINJAM'")

                Tampung.DefaultView.Sort = "Tanggal, NomorPeminjaman"
                GridControl1.DataSource = Tampung
                ExecuteGridViewAllAppearance(GridView1)

            Case 1
                Tampung = ExecuteQuery("select a.Tanggal, a.NomorPengembalian, d.Jumlah, a.NIP, b.NamaPegawai, a.KodeRuang, a.TanggalKembali, a.StatusPinjam, a.KeteranganKembali from (peminjaman a inner join pegawai b on a.NIP=b.NIP inner join pengembalian_detil d on a.NomorPengembalian=d.NomorPengembalian) where date_format(a.Tanggal, '%Y-%m-%d') between '" & Format(txtTanggalAwal.DateTime, "yyyy-MM-dd") & "' and '" & Format(txtTanggalAkhir.DateTime, "yyyy-MM-dd") & "' and StatusPinjam='DIKEMBALIKAN'")

                Tampung.DefaultView.Sort = "Tanggal, NomorPengembalian"
                GridControl1.DataSource = Tampung
                ExecuteGridViewAllAppearance(GridView1)
        End Select
    End Sub

    Sub Cetak()
        Select Case txtKategori.SelectedIndex
            Case 0
                GridView1.OptionsPrint.RtfReportHeader = "LAPORAN PER PEMINJAMAN" & vbCrLf & "Periode" & Format(txtTanggalAwal.DateTime, "dd MMMM yyyy") & " s/d " & Format(txtTanggalAkhir.DateTime, "dd MMMM yyyy") & "" & vbCrLf
                GridView1.OptionsPrint.RtfReportFooter = vbCrLf & "Pimpinan" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & "I Gede Yuda Pratama"
                ExecuteGridControlPreview(GridControl1, GridControl1.LookAndFeel, True, Printing.PaperKind.Folio, 10, 10, 10, 10)
            Case 1
                GridView1.OptionsPrint.RtfReportHeader = "LAPORAN PER PENGEMBALIAN" & vbCrLf & "Periode" & Format(txtTanggalAwal.DateTime, "dd MMMM yyyy") & " s/d " & Format(txtTanggalAkhir.DateTime, "dd MMMM yyyy") & "" & vbCrLf
                GridView1.OptionsPrint.RtfReportFooter = vbCrLf & "Pimpinan" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & "I Gede Yuda Pratama"
                ExecuteGridControlPreview(GridControl1, GridControl1.LookAndFeel, True, Printing.PaperKind.Folio, 10, 10, 10, 10)
        End Select
    End Sub

    Sub Tutup()
        Me.Close()

    End Sub

    Private Sub frmLaporanInventaris_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case e.Control And Keys.Enter
                TampilTabel()
            Case e.Control And Keys.P
                TampilTabel()
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub frmLaporanInventaris_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtKategori.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        txtTanggalAwal.DateTime = Now
        txtTanggalAkhir.DateTime = Now
        txtKategori.SelectedIndex = 0
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        TampilTabel()

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Cetak()

    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Tutup()

    End Sub
End Class