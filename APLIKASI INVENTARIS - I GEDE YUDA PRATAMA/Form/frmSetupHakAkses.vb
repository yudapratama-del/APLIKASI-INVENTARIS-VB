Public Class frmSetupHakAkses 
    Dim Tampung As New DataTable
    Dim StatusDataBaru As Boolean

    Sub TampilTabel()
        Tampung = ExecuteQuery("select * from hak_akses")
        GridControl1.DataSource = Tampung
        ExecuteGridViewAllAppearance(GridView1, , "NilaiHakAkses")
    End Sub

    Sub DataBaru()
        StatusDataBaru = True
        ExecuteInputTextValueClear(txtNamaHakAkses)
        ExecuteTreeViewCheckUncheck(frmSetupMenuUtama.RibbonControl, txtNilaiHakAkses, True)
        txtNamaHakAkses.Properties.ReadOnly = False
        txtNamaHakAkses.TabStop = True
        TampilTabel()
        txtNamaHakAkses.Focus()
    End Sub

    Sub Simpan()
        If txtNamaHakAkses.Text = "" Then
            MessageBox.Show("Nama Hak Akses Wajib Diisi!", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If StatusDataBaru = True Then
            Dim Cari = Tampung.Select("NamaHakAkses='" & txtNamaHakAkses.Text & "'")
            If Cari.Length <= 0 Then
                'koding simpan
                Dim BarisBaru = Tampung.NewRow
                BarisBaru.Item("NamaHakAkses") = txtNamaHakAkses.Text
                BarisBaru.Item("NilaiHakAkses") = ExecuteGetAccess(txtNilaiHakAkses)
                Tampung.Rows.Add(BarisBaru)
                ExecuteQueryUpdate(Tampung, "hak_akses")
                DataBaru()
                MessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data Gagal Disimpan." & vbCrLf & "Nama Hak Akses sudah ada dalam database.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            Dim Cari = Tampung.Select("NamaHakAkses='" & txtNamaHakAkses.Text & "'")
            Cari(0).Item("NilaiHakAkses") = ExecuteGetAccess(txtNilaiHakAkses)
            ExecuteQueryUpdate(Tampung, "hak_akses")
            DataBaru()
            MessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Sub Ubah()
        If GridView1.RowCount > 0 Then
            StatusDataBaru = False
            txtNamaHakAkses.Text = GridView1.GetFocusedRowCellValue("NamaHakAkses")
            ExecuteSetAccessTreeView(frmSetupMenuUtama.RibbonControl, txtNilaiHakAkses, GridView1.GetFocusedRowCellValue("NilaiHakAkses"))
            txtNamaHakAkses.Properties.ReadOnly = True
            txtNamaHakAkses.TabStop = False
            txtNilaiHakAkses.Focus()
        Else
            MessageBox.Show("Tidak ada data yang dipilih", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Hapus()
        If GridView1.RowCount > 0 Then
            If MessageBox.Show("Data akan dihapus. Lanjutkan?", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                GridView1.DeleteSelectedRows()
                ExecuteQueryUpdate(Tampung, "hak_akses", GridView1)
                DataBaru()

            End If
        Else
            MessageBox.Show("Tidak ada data yang dipilih!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Cetak()
        GridControl1.ShowRibbonPrintPreview()
    End Sub

    Sub CheckAll()
        ExecuteTreeViewCheckUncheck(frmSetupMenuUtama.RibbonControl, txtNilaiHakAkses, True)
    End Sub

    Sub UncheckAll()
        ExecuteTreeViewCheckUncheck(frmSetupMenuUtama.RibbonControl, txtNilaiHakAkses, False)
    End Sub

    Private Sub frmSetupHakAkses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case e.Control And Keys.Enter
                Simpan()
            Case Keys.F5
                DataBaru()
            Case Keys.Escape
                Me.Close()
            Case Keys.F2
                Ubah()
            Case Keys.Control And Keys.Delete
                Hapus()
            Case Keys.F8
                Cetak()
            Case Keys.F6
                CheckAll()
            Case Keys.F7
                UncheckAll()

        End Select
    End Sub

    Private Sub frmHakAkses_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtNilaiHakAkses.CheckBoxes = True
        DataBaru()

    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Simpan()

    End Sub

    Private Sub BarButtonItem1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Ubah()

    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        DataBaru()

        Refresh()

    End Sub

    Private Sub SimpleButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton6.Click
        Me.Close()

    End Sub

    Private Sub BarButtonItem2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Hapus()

    End Sub

    Private Sub BarButtonItem3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Cetak()

    End Sub

    Private Sub BarButtonItem4_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        CheckAll()

    End Sub

    Private Sub BarButtonItem5_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        UncheckAll()

    End Sub


End Class