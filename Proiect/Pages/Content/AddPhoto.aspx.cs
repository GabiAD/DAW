using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Content_AddPhoto : System.Web.UI.Page
{
    protected int len;
    protected byte[] photoData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated))
        {
            return;
        }

        if (!Page.IsPostBack)
        {
            MembershipUser membershipUser = Membership.GetUser();
            var userId = membershipUser.ProviderUserKey;

            var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
            var comm = new SqlCommand("SELECT Id, Nume FROM Albume WHERE UserId = @uid", conn);
            comm.Parameters.AddWithValue("uid", userId);

            var dataTable = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(dataTable);

            AlbumDD.DataSource = dataTable;
            AlbumDD.DataTextField = "Nume";
            AlbumDD.DataValueField = "Id";
            AlbumDD.DataBind();

            comm = new SqlCommand("SELECT Id, Tag FROM Tags", conn);
            adapter = new SqlDataAdapter(comm);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            TagDD.DataSource = dataTable;
            TagDD.DataTextField = "Tag";
            TagDD.DataValueField = "Id";
            TagDD.DataBind();

            EmptySessionPhoto();
        }
        else
        {
            ApplyFilters();
        }

    }

    protected void OnFileUploadChange(object sender, EventArgs e)
    {
        Descriere.InnerText += "...";
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        string qry = "INSERT INTO Poze (Imagine, Descriere, DataPostare, Titlu, AlbumId, TagId) VALUES(@img, @desc, @data, @titl, @albId, @tagId)";

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='D:\FMI 3.1\DAW\Proiect\App_Data\Database.mdf';Integrated Security=True");
        SqlCommand sqlComm = new SqlCommand(qry, conn);

        sqlComm.Parameters.AddWithValue("img", Convert.FromBase64String(Session["EditedPhoto"].ToString()));
        sqlComm.Parameters.AddWithValue("desc", Descriere.InnerText);
        sqlComm.Parameters.AddWithValue("data", DateTime.Now);
        sqlComm.Parameters.AddWithValue("titl", TitluPozaNoua.Text);
        sqlComm.Parameters.AddWithValue("albId", AlbumDD.SelectedValue);
        sqlComm.Parameters.AddWithValue("tagId", TagDD.SelectedValue);

        conn.Open();
        try
        {
            sqlComm.ExecuteNonQuery();
            message.Visible = true;
        }
        catch (Exception exc)
        {
                
        }

        conn.Close();
    }

    private void EmptySessionPhoto()
    {
        Session["InitialPhoto"] = null;
        Session["EditedPhoto"] = null;
    }

    private void ApplyFilters()
    {
        if (Session["InitialPhoto"] == null && PhotoUpload.HasFile)
        {
            Stream photoStream = PhotoUpload.PostedFile.InputStream;
            int photoLength = PhotoUpload.PostedFile.ContentLength;
            byte[] photoData = new byte[photoLength];
            photoStream.Read(photoData, 0, photoLength);

            Session["InitialPhoto"] = Convert.ToBase64String(photoData);
            Session["EditedPhoto"] = Convert.ToBase64String(photoData);
        }

        if (Session["InitialPhoto"] != null)
        {
            var img = Convert.FromBase64String(Session["InitialPhoto"].ToString());

            if (FlipOpt.Checked)
            {
                img = Flip(img);
            }
            if (BlackAndWhite.Checked)
            {
                img = SetGrayscale(img);
            }
            img = SetContrast(img, int.Parse(Contrast.Text));

            Session["EditedPhoto"] = Convert.ToBase64String(img);
            PreviewImage.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(img);
        }
    }

    public byte[] Flip(byte[] imgB)
    {
        var ms = new MemoryStream(imgB);
        System.Drawing.Image _currentBitmap = System.Drawing.Image.FromStream(ms);

        var rotateFlipType = RotateFlipType.Rotate180FlipY;

        Bitmap temp = (Bitmap)_currentBitmap;
        Bitmap bmap = (Bitmap)temp.Clone();
        bmap.RotateFlip(rotateFlipType);
        _currentBitmap = (Bitmap)bmap.Clone();

        ImageConverter converter = new ImageConverter();
        return (byte[])converter.ConvertTo(_currentBitmap, typeof(byte[]));
    }

    public byte[] SetGrayscale(byte[] imgB)
    {
        var ms = new MemoryStream(imgB);
        System.Drawing.Image _currentBitmap = System.Drawing.Image.FromStream(ms);

        Bitmap temp = (Bitmap)_currentBitmap;
        Bitmap bmap = (Bitmap)temp.Clone();
        Color c;
        for (int i = 0; i < bmap.Width; i++)
        {
            for (int j = 0; j < bmap.Height; j++)
            {
                c = bmap.GetPixel(i, j);
                byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
            }
        }
        _currentBitmap = (Bitmap)bmap.Clone();

        ImageConverter converter = new ImageConverter();
        return (byte[])converter.ConvertTo(_currentBitmap, typeof(byte[]));
    }

    private byte[] SetContrast(byte[] imgB, double contrast)
    {
        var ms = new MemoryStream(imgB);
        System.Drawing.Image _currentBitmap = System.Drawing.Image.FromStream(ms);
        
        Bitmap temp = (Bitmap)_currentBitmap;
        Bitmap bmap = (Bitmap)temp.Clone();
        if (contrast < -100) contrast = -100;
        if (contrast > 100) contrast = 100;
        contrast = (100.0 + contrast) / 100.0;
        contrast *= contrast;
        Color c;
        for (int i = 0; i < bmap.Width; i++)
        {
            for (int j = 0; j < bmap.Height; j++)
            {
                c = bmap.GetPixel(i, j);
                double pR = c.R / 255.0;
                pR -= 0.5;
                pR *= contrast;
                pR += 0.5;
                pR *= 255;
                if (pR < 0) pR = 0;
                if (pR > 255) pR = 255;

                double pG = c.G / 255.0;
                pG -= 0.5;
                pG *= contrast;
                pG += 0.5;
                pG *= 255;
                if (pG < 0) pG = 0;
                if (pG > 255) pG = 255;

                double pB = c.B / 255.0;
                pB -= 0.5;
                pB *= contrast;
                pB += 0.5;
                pB *= 255;
                if (pB < 0) pB = 0;
                if (pB > 255) pB = 255;

                bmap.SetPixel(i, j,
                Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
            }
        }
        _currentBitmap = (Bitmap)bmap.Clone();

        ImageConverter converter = new ImageConverter();
        return (byte[])converter.ConvertTo(_currentBitmap, typeof(byte[]));
    }

    protected void PhotoValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = Session["EditedPhoto"] != null;
    }
    
}