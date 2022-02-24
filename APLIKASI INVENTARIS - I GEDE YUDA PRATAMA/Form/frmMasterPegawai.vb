Public Class frmMasterPegawai 
    Dim Tampung As New DataTable
    Dim StatusDataBaru As Boolean

    Sub TampilTabel()
        Tampung = ExecuteQuery("select NIP,NamaPegawai,Jabatan,Alamat,NomorTelepon,Keterangan from pegawai")
        GridControl1.DataSource = Tampung
        ExecuteGridViewAllAppearance(GridView1)
    End Sub

    Sub DataBaru()
        StatusDataBaru = True
        ExecuteInputTextValueClear(txtNamaPegawai, txtJabatan, txtAlamat, txtNoTelp, txtKet)
        txtNIP.Text = ExecuteAutoCode("pegawai", "NIP", "NIP", "000000")
        TampilTabel()
        txtNamaPegawai.Focus()
        txtNoTelp.Text = "+62"
    End Sub

    Sub Simpan()
        If txtNIP.Text = "" Or txtNamaPegawai.Text = "" Or txtJabatan.Text = "" Then
            MessageBox.Show("Kode Konsumen, Nama Konsumen Jabatan dan Alamat Harus Diisi!!", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If StatusDataBaru = True Then
            Dim Cari = Tampung.Select("NIP='" & txtNIP.Text & "'")
            If Cari.Length <= 0 Then
                'INSERT
                ExecuteQuery("INSERT INTO pegawai(NIP,NamaPegawai,Jabatan,Alamat,NomorTelepon,Keterangan) VALUES ('" & txtNIP.Text & "','" & txtNamaPegawai.Text & "','" & txtJabatan.Text & "','" & txtAlamat.Text & "','" & txtNoTelp.Text & "','" & txtKet.Text & "')")
                DataBaru()
                MessageBox.Show("DataBerhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data Gagal Disimpan." & vbCrLf & "Data sudah ada dalam database.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            'UPDATE
            ExecuteQuery("UPDATE pegawai SET NamaPegawai='" & txtNamaPegawai.Text & "',Jabatan='" & txtJabatan.Text & "',Alamat='" & txtAlamat.Text & "',NomorTelepon='" & txtNoTelp.Text & "',Keterangan='" & txtKet.Text & "' WHERE NIP='" & txtNIP.Text & "' ")
            DataBaru()
            MessageBox.Show("DataBerhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Sub Ubah()
        If GridView1.RowCount > 0 Then
            StatusDataBaru = False
            txtNIP.Text = GridView1.GetFocusedRowCellValue("NIP")
            txtNamaPegawai.Text = GridView1.GetFocusedRowCellValue("NamaPegawai")
            txtJabatan.Text = GridView1.GetFocusedRowCellValue("Jabatan")
            txtAlamat.Text = GridView1.GetFocusedRowCellValue("Alamat")
            txtNoTelp.Text = GridView1.GetFocusedRowCellValue("NomorTelepon")
            txtKet.Text = GridView1.GetFocusedRowCellValue("Keterangan")
            txtNamaPegawai.Focus()
        Else
            MessageBox.Show("Tidak ada data yang dipilih", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Hapus()
        If GridView1.RowCount > 0 Then
            If MessageBox.Show("Data akan dihapus. Lanjutkan?", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'DELETE
                ExecuteQuery("DELETE FROM pegawai WHERE NIP='" & GridView1.GetFocusedRowCellValue("NIP") & "'")
                DataBaru()
            End If
        Else
            MessageBox.Show("Tidak ada data yang dipilih!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Cetak()
        GridControl1.ShowRibbonPrintPreview()
    End Sub

    Private Sub frmMasterPegawai_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case e.Control And Keys.Enter
                Simpan()
            Case Keys.F5
                DataBaru()
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

    Private Sub frmMasterPegawai_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtNIP.ReadOnly = True
        txtNIP.TabStop = False
        DataBaru()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        DataBaru()

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Simpan()

    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        Me.Close()

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Ubah()

    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Hapus()

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Cetak()

    End Sub

    Private Sub txtJabatan_GotFocus(sender As Object, e As EventArgs) Handles txtJabatan.GotFocus
        ExecuteComboBoxList("pegawai", "Jabatan", txtJabatan)
    End Sub

    Private Sub txtJabatan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtJabatan.SelectedIndexChanged

    End Sub
End Class