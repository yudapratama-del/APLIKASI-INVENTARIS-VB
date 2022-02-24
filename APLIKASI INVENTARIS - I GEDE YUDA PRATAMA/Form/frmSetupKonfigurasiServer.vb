Imports MySql.Data.MySqlClient

Public Class frmSetupKonfigurasiServer

    Sub TesKonek()
        'Valid Input
        If txtNamaServer.Text = "" Or txtUserID.Text = "" Or txtNamaDatabase.Text = "" Then
            MessageBox.Show("Nama Server, User ID, dan Nama Database harus diisi!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub 'Otomatis Keluar Sub
        End If

        Dim tempConString As String = My.Settings.ConString
        My.Settings.ConString = "server=" & txtNamaServer.Text & ";user ID=" & txtUserID.Text & ";password=" & txtPassword.Text & ";database=" & txtNamaDatabase.Text & ""
        Try
            ExecuteConnection(True)
            MessageBox.Show("Koneksi Berhasil", "Informasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Koneksi Gagal." & vbCrLf & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        My.Settings.ConString = tempConString
    End Sub

    Sub Simpan()
        'Valid Input
        If txtNamaServer.Text = "" Or txtUserID.Text = "" Or txtNamaDatabase.Text = "" Then
            MessageBox.Show("Nama Server, User ID, dan Nama Database harus diisi!", "Validasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub 'Otomatis Keluar Sub
        End If

        Dim tempConString As String = My.Settings.ConString
        My.Settings.ConString = "server=" & txtNamaServer.Text & ";user ID=" & txtUserID.Text & ";password=" & txtPassword.Text & ";database=" & txtNamaDatabase.Text & ""
        Try
            ExecuteConnection(True)
            'menyimpan konfigurasi
            My.Settings.ConNamaServer = txtNamaServer.Text
            My.Settings.ConUserID = txtUserID.Text
            My.Settings.ConPassword = txtPassword.Text
            My.Settings.ConNamaDatabase = txtNamaDatabase.Text
            My.Settings.Save()
            Me.Close()
        Catch ex As Exception
            My.Settings.ConString = tempConString
            MessageBox.Show("Gagal Tersimpan." & vbCrLf & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub Tutup()
        Me.Close()
    End Sub

    Private Sub frmSetupKonfigurasiServer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case e.Control And Keys.Enter
                Simpan()
            Case Keys.Escape
                Tutup()
            Case Keys.F1
                TesKonek()
        End Select

    End Sub

    Private Sub frmSetupKonfigurasiServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtPassword.Properties.UseSystemPasswordChar = True

        'menampilkan hasil konfigurasi server
        txtNamaServer.Text = My.Settings.ConNamaServer
        txtUserID.Text = My.Settings.ConUserID
        txtPassword.Text = My.Settings.ConPassword
        txtNamaDatabase.Text = My.Settings.ConNamaDatabase
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Simpan()
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Tutup()
    End Sub

    Private Sub BarButtonItem1_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        TesKonek()
    End Sub


End Class