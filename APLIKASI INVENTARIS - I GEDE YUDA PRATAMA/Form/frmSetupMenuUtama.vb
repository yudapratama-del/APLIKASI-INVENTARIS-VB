Public Class frmSetupMenuUtama 
    Private Sub frmSetupMenuUtama_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'memastikan aplikasi tertutup jika form login tidak ditampilkan
        If frmSetupLogin.Visible = False Then
            Application.Exit()
        End If
    End Sub


    Private Sub frmSetupMenuUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        ExecuteRibbonControlStyle(RibbonControl)
        ExecuteWalpaper(Me, My.Settings.ImageLocation)
        ExecuteSkin(My.Settings.SkinName, RibbonGalleryBarItem1)
        BarButtonItem3.Caption = "Pengguna : " & My.Settings.lgnNamaPengguna
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        BarButtonItem4.Caption = "Jam : " & Format(Now, "HH:mm:ss") & " Hari : " & Format(Now, "dddd") & " Tanggal : " & Format(Now, "dd MMMM yyyy")
    End Sub

    Private Sub BarButtonItem1_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        'logout
        If MessageBox.Show("Akses Pengguna Akan Ditutup !" & vbCrLf & "Lanjutkan?", "Verifikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
            With frmSetupLogin
                .Show()
                Me.Close()
            End With
        End If
    End Sub

    Private Sub BarButtonItem2_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        'keluar
        If frmSetupLogin.Visible <> True Then
            If MessageBox.Show("Aplikasi Akan Ditutup !" & vbCrLf & "Lanjutkan?", "Verifikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                Application.Exit()
            End If
        End If
    End Sub

    Private Sub RibbonGalleryBarItem1_GalleryItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs) Handles RibbonGalleryBarItem1.GalleryItemClick
        ExecuteSkinSave(e.Item.Caption)
    End Sub

    Private Sub BarButtonItem5_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        ExecuteWalpaperSave(Me)
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        frmSetupKonfigurasiServer.ShowDialog()
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        frmSetupHakAkses.ShowDialog()
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        'pending
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        frmSetupPengguna.MdiParent = Me
        frmSetupPengguna.Show()

    End Sub

    Private Sub BarButtonItem12_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem12.ItemClick
        frmMasterInventaris.MdiParent = Me
        frmMasterInventaris.Show()

    End Sub

    Private Sub BarButtonItem13_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem13.ItemClick
        frmMasterJenis.MdiParent = Me
        frmMasterJenis.Show()

    End Sub

    Private Sub BarButtonItem14_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem14.ItemClick
        frmMasterRuangPenyimpanan.MdiParent = Me
        frmMasterRuangPenyimpanan.Show()

    End Sub

    Private Sub BarButtonItem15_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem15.ItemClick
        frmMasterPegawai.MdiParent = Me
        frmMasterPegawai.Show()

    End Sub

    Private Sub BarButtonItem18_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem18.ItemClick
        frmMasterRuangPeminjaman.MdiParent = Me
        frmMasterRuangPeminjaman.Show()
    End Sub

    Private Sub BarButtonItem16_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem16.ItemClick
        frmTransaksiPeminjaman.MdiParent = Me
        frmTransaksiPeminjaman.Show()

    End Sub

    Private Sub BarButtonItem17_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem17.ItemClick
        frmTransaksiPengembalian.MdiParent = Me
        frmTransaksiPengembalian.Show()
    End Sub

    Private Sub BarButtonItem19_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem19.ItemClick
        frmLaporanInventaris.MdiParent = Me
        frmLaporanInventaris.Show()
    End Sub
End Class