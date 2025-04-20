namespace BonfieFood
{
    partial class Scanner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scanner));
            this.homePageImg = new Guna.UI2.WinForms.Guna2PictureBox();
            this.uploadPhotoHover = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btn_ScanPhoto = new Guna.UI2.WinForms.Guna2Button();
            this.label_Clarifai = new System.Windows.Forms.Label();
            this.deletePhoto = new Guna.UI2.WinForms.Guna2PictureBox();
            this.uploadedUserPoto = new Guna.UI2.WinForms.Guna2PictureBox();
            this.res_totalCalories = new System.Windows.Forms.Label();
            this.res_iron = new System.Windows.Forms.Label();
            this.res_iron_value = new System.Windows.Forms.Label();
            this.res_calcium = new System.Windows.Forms.Label();
            this.res_cholesterol = new System.Windows.Forms.Label();
            this.res_carbohydrates = new System.Windows.Forms.Label();
            this.res_fat = new System.Windows.Forms.Label();
            this.res_protein = new System.Windows.Forms.Label();
            this.res_cholesterol_value = new System.Windows.Forms.Label();
            this.res_calcium_value = new System.Windows.Forms.Label();
            this.res_fat_value = new System.Windows.Forms.Label();
            this.res_carbohydrates_value = new System.Windows.Forms.Label();
            this.res_totalCaories_value = new System.Windows.Forms.Label();
            this.res_protein_value = new System.Windows.Forms.Label();
            this.res_products = new System.Windows.Forms.Label();
            this.line = new Guna.UI2.WinForms.Guna2Panel();
            this.res_dishName = new System.Windows.Forms.Label();
            this.res_userPhotoDish = new Guna.UI2.WinForms.Guna2PictureBox();
            this.toolTip_uploadPhoto = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.info_dish = new FontAwesome.Sharp.IconPictureBox();
            this.toolTip_infoDish = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.toolTip_Products = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.closeResults = new Guna.UI2.WinForms.Guna2Button();
            this.resultsAnalysis = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.homePageImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uploadPhotoHover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deletePhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uploadedUserPoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.res_userPhotoDish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.info_dish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsAnalysis)).BeginInit();
            this.SuspendLayout();
            // 
            // homePageImg
            // 
            this.homePageImg.BackColor = System.Drawing.Color.Transparent;
            this.homePageImg.Cursor = System.Windows.Forms.Cursors.Default;
            this.homePageImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homePageImg.FillColor = System.Drawing.Color.Transparent;
            this.homePageImg.Image = global::BonfieFood.Properties.Resources.upload_image;
            this.homePageImg.ImageRotate = 0F;
            this.homePageImg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.homePageImg.Location = new System.Drawing.Point(0, 0);
            this.homePageImg.Name = "homePageImg";
            this.homePageImg.Size = new System.Drawing.Size(720, 500);
            this.homePageImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.homePageImg.TabIndex = 13;
            this.homePageImg.TabStop = false;
            this.homePageImg.UseTransparentBackground = true;
            // 
            // uploadPhotoHover
            // 
            this.uploadPhotoHover.BackColor = System.Drawing.Color.Transparent;
            this.uploadPhotoHover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uploadPhotoHover.FillColor = System.Drawing.Color.Transparent;
            this.uploadPhotoHover.ImageRotate = 0F;
            this.uploadPhotoHover.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.uploadPhotoHover.Location = new System.Drawing.Point(274, 125);
            this.uploadPhotoHover.Name = "uploadPhotoHover";
            this.uploadPhotoHover.Size = new System.Drawing.Size(174, 166);
            this.uploadPhotoHover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.uploadPhotoHover.TabIndex = 14;
            this.uploadPhotoHover.TabStop = false;
            this.uploadPhotoHover.UseTransparentBackground = true;
            this.uploadPhotoHover.Click += new System.EventHandler(this.uploadPhotoHover_Click);
            // 
            // btn_ScanPhoto
            // 
            this.btn_ScanPhoto.Animated = true;
            this.btn_ScanPhoto.BackColor = System.Drawing.Color.Transparent;
            this.btn_ScanPhoto.BorderRadius = 17;
            this.btn_ScanPhoto.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_ScanPhoto.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ScanPhoto.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ScanPhoto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ScanPhoto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ScanPhoto.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(254)))));
            this.btn_ScanPhoto.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold);
            this.btn_ScanPhoto.ForeColor = System.Drawing.Color.White;
            this.btn_ScanPhoto.Location = new System.Drawing.Point(283, 420);
            this.btn_ScanPhoto.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.btn_ScanPhoto.Name = "btn_ScanPhoto";
            this.btn_ScanPhoto.ShadowDecoration.BorderRadius = 18;
            this.btn_ScanPhoto.ShadowDecoration.Color = System.Drawing.Color.Empty;
            this.btn_ScanPhoto.ShadowDecoration.Depth = 99;
            this.btn_ScanPhoto.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(22);
            this.btn_ScanPhoto.Size = new System.Drawing.Size(145, 40);
            this.btn_ScanPhoto.TabIndex = 85;
            this.btn_ScanPhoto.Text = "Scan photo";
            this.btn_ScanPhoto.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            this.btn_ScanPhoto.UseTransparentBackground = true;
            this.btn_ScanPhoto.Click += new System.EventHandler(this.btn_ScanPhoto_Click);
            // 
            // label_Clarifai
            // 
            this.label_Clarifai.BackColor = System.Drawing.Color.Transparent;
            this.label_Clarifai.Font = new System.Drawing.Font("Leelawadee UI", 12F);
            this.label_Clarifai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_Clarifai.Location = new System.Drawing.Point(128, 315);
            this.label_Clarifai.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label_Clarifai.Name = "label_Clarifai";
            this.label_Clarifai.Size = new System.Drawing.Size(457, 75);
            this.label_Clarifai.TabIndex = 86;
            this.label_Clarifai.Text = "Clarifai AI model will recognize the dishes and products in the photo, providing " +
    "detailed information about their composition and CPFC. Control your diet easily " +
    "and conveniently!";
            this.label_Clarifai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deletePhoto
            // 
            this.deletePhoto.BackColor = System.Drawing.Color.Transparent;
            this.deletePhoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deletePhoto.FillColor = System.Drawing.Color.Transparent;
            this.deletePhoto.Image = global::BonfieFood.Properties.Resources.remove_scanner;
            this.deletePhoto.ImageFlip = Guna.UI2.WinForms.Enums.FlipOrientation.Horizontal;
            this.deletePhoto.ImageRotate = 0F;
            this.deletePhoto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.deletePhoto.Location = new System.Drawing.Point(458, 20);
            this.deletePhoto.Name = "deletePhoto";
            this.deletePhoto.Size = new System.Drawing.Size(30, 30);
            this.deletePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.deletePhoto.TabIndex = 91;
            this.deletePhoto.TabStop = false;
            this.deletePhoto.UseTransparentBackground = true;
            this.deletePhoto.Click += new System.EventHandler(this.deletePhoto_Click);
            this.deletePhoto.MouseEnter += new System.EventHandler(this.deletePhoto_MouseEnter);
            this.deletePhoto.MouseLeave += new System.EventHandler(this.deletePhoto_MouseLeave);
            this.deletePhoto.MouseHover += new System.EventHandler(this.deletePhoto_MouseHover);
            // 
            // uploadedUserPoto
            // 
            this.uploadedUserPoto.BackColor = System.Drawing.Color.Transparent;
            this.uploadedUserPoto.BorderRadius = 30;
            this.uploadedUserPoto.Cursor = System.Windows.Forms.Cursors.Default;
            this.uploadedUserPoto.FillColor = System.Drawing.Color.Transparent;
            this.uploadedUserPoto.ImageRotate = 0F;
            this.uploadedUserPoto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.uploadedUserPoto.Location = new System.Drawing.Point(240, 28);
            this.uploadedUserPoto.Name = "uploadedUserPoto";
            this.uploadedUserPoto.Size = new System.Drawing.Size(240, 260);
            this.uploadedUserPoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.uploadedUserPoto.TabIndex = 90;
            this.uploadedUserPoto.TabStop = false;
            this.uploadedUserPoto.UseTransparentBackground = true;
            // 
            // res_totalCalories
            // 
            this.res_totalCalories.AutoSize = true;
            this.res_totalCalories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_totalCalories.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_totalCalories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(241)))), ((int)(((byte)(253)))));
            this.res_totalCalories.Location = new System.Drawing.Point(181, 209);
            this.res_totalCalories.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_totalCalories.Name = "res_totalCalories";
            this.res_totalCalories.Size = new System.Drawing.Size(105, 32);
            this.res_totalCalories.TabIndex = 192;
            this.res_totalCalories.Text = "Calories";
            // 
            // res_iron
            // 
            this.res_iron.AutoSize = true;
            this.res_iron.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_iron.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_iron.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_iron.Location = new System.Drawing.Point(183, 385);
            this.res_iron.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_iron.Name = "res_iron";
            this.res_iron.Size = new System.Drawing.Size(41, 21);
            this.res_iron.TabIndex = 190;
            this.res_iron.Text = "Iron";
            // 
            // res_iron_value
            // 
            this.res_iron_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_iron_value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_iron_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_iron_value.Location = new System.Drawing.Point(443, 385);
            this.res_iron_value.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_iron_value.Name = "res_iron_value";
            this.res_iron_value.Size = new System.Drawing.Size(79, 21);
            this.res_iron_value.TabIndex = 189;
            this.res_iron_value.Text = "0.0 mg";
            this.res_iron_value.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // res_calcium
            // 
            this.res_calcium.AutoSize = true;
            this.res_calcium.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_calcium.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_calcium.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_calcium.Location = new System.Drawing.Point(183, 359);
            this.res_calcium.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_calcium.Name = "res_calcium";
            this.res_calcium.Size = new System.Drawing.Size(72, 21);
            this.res_calcium.TabIndex = 188;
            this.res_calcium.Text = "Calcium";
            // 
            // res_cholesterol
            // 
            this.res_cholesterol.AutoSize = true;
            this.res_cholesterol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_cholesterol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_cholesterol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_cholesterol.Location = new System.Drawing.Point(183, 333);
            this.res_cholesterol.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_cholesterol.Name = "res_cholesterol";
            this.res_cholesterol.Size = new System.Drawing.Size(97, 21);
            this.res_cholesterol.TabIndex = 186;
            this.res_cholesterol.Text = "Cholesterol";
            // 
            // res_carbohydrates
            // 
            this.res_carbohydrates.AutoSize = true;
            this.res_carbohydrates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_carbohydrates.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_carbohydrates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_carbohydrates.Location = new System.Drawing.Point(183, 307);
            this.res_carbohydrates.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_carbohydrates.Name = "res_carbohydrates";
            this.res_carbohydrates.Size = new System.Drawing.Size(163, 21);
            this.res_carbohydrates.TabIndex = 185;
            this.res_carbohydrates.Text = "Total Carbohydrates";
            // 
            // res_fat
            // 
            this.res_fat.AutoSize = true;
            this.res_fat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_fat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_fat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_fat.Location = new System.Drawing.Point(183, 281);
            this.res_fat.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_fat.Name = "res_fat";
            this.res_fat.Size = new System.Drawing.Size(73, 21);
            this.res_fat.TabIndex = 184;
            this.res_fat.Text = "Total fat";
            // 
            // res_protein
            // 
            this.res_protein.AutoSize = true;
            this.res_protein.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_protein.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_protein.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_protein.Location = new System.Drawing.Point(183, 255);
            this.res_protein.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_protein.Name = "res_protein";
            this.res_protein.Size = new System.Drawing.Size(66, 21);
            this.res_protein.TabIndex = 183;
            this.res_protein.Text = "Protein";
            // 
            // res_cholesterol_value
            // 
            this.res_cholesterol_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_cholesterol_value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_cholesterol_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_cholesterol_value.Location = new System.Drawing.Point(443, 333);
            this.res_cholesterol_value.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_cholesterol_value.Name = "res_cholesterol_value";
            this.res_cholesterol_value.Size = new System.Drawing.Size(79, 21);
            this.res_cholesterol_value.TabIndex = 182;
            this.res_cholesterol_value.Text = "12 mg";
            this.res_cholesterol_value.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // res_calcium_value
            // 
            this.res_calcium_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_calcium_value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_calcium_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_calcium_value.Location = new System.Drawing.Point(443, 359);
            this.res_calcium_value.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_calcium_value.Name = "res_calcium_value";
            this.res_calcium_value.Size = new System.Drawing.Size(79, 21);
            this.res_calcium_value.TabIndex = 180;
            this.res_calcium_value.Text = "0.0 mg";
            this.res_calcium_value.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // res_fat_value
            // 
            this.res_fat_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_fat_value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_fat_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_fat_value.Location = new System.Drawing.Point(458, 281);
            this.res_fat_value.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_fat_value.Name = "res_fat_value";
            this.res_fat_value.Size = new System.Drawing.Size(64, 21);
            this.res_fat_value.TabIndex = 179;
            this.res_fat_value.Text = "0 g";
            this.res_fat_value.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // res_carbohydrates_value
            // 
            this.res_carbohydrates_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_carbohydrates_value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_carbohydrates_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_carbohydrates_value.Location = new System.Drawing.Point(458, 307);
            this.res_carbohydrates_value.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_carbohydrates_value.Name = "res_carbohydrates_value";
            this.res_carbohydrates_value.Size = new System.Drawing.Size(64, 21);
            this.res_carbohydrates_value.TabIndex = 178;
            this.res_carbohydrates_value.Text = "0 g";
            this.res_carbohydrates_value.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // res_totalCaories_value
            // 
            this.res_totalCaories_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_totalCaories_value.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_totalCaories_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(241)))), ((int)(((byte)(253)))));
            this.res_totalCaories_value.Location = new System.Drawing.Point(392, 209);
            this.res_totalCaories_value.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_totalCaories_value.Name = "res_totalCaories_value";
            this.res_totalCaories_value.Size = new System.Drawing.Size(130, 32);
            this.res_totalCaories_value.TabIndex = 177;
            this.res_totalCaories_value.Text = "90 cal";
            this.res_totalCaories_value.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // res_protein_value
            // 
            this.res_protein_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_protein_value.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_protein_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_protein_value.Location = new System.Drawing.Point(458, 255);
            this.res_protein_value.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.res_protein_value.Name = "res_protein_value";
            this.res_protein_value.Size = new System.Drawing.Size(64, 21);
            this.res_protein_value.TabIndex = 176;
            this.res_protein_value.Text = "0 g";
            this.res_protein_value.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // res_products
            // 
            this.res_products.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_products.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_products.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.res_products.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.res_products.Location = new System.Drawing.Point(297, 89);
            this.res_products.Margin = new System.Windows.Forms.Padding(3);
            this.res_products.Name = "res_products";
            this.res_products.Size = new System.Drawing.Size(225, 89);
            this.res_products.TabIndex = 175;
            this.res_products.Text = "Products:";
            // 
            // line
            // 
            this.line.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.line.Location = new System.Drawing.Point(158, 195);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(402, 1);
            this.line.TabIndex = 174;
            // 
            // res_dishName
            // 
            this.res_dishName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.res_dishName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.res_dishName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.res_dishName.Location = new System.Drawing.Point(295, 52);
            this.res_dishName.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.res_dishName.Name = "res_dishName";
            this.res_dishName.Size = new System.Drawing.Size(258, 32);
            this.res_dishName.TabIndex = 173;
            this.res_dishName.Text = "Dish name";
            // 
            // res_userPhotoDish
            // 
            this.res_userPhotoDish.BackColor = System.Drawing.Color.Transparent;
            this.res_userPhotoDish.BorderRadius = 30;
            this.res_userPhotoDish.Cursor = System.Windows.Forms.Cursors.Default;
            this.res_userPhotoDish.FillColor = System.Drawing.Color.Gray;
            this.res_userPhotoDish.ImageRotate = 0F;
            this.res_userPhotoDish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.res_userPhotoDish.Location = new System.Drawing.Point(176, 46);
            this.res_userPhotoDish.Name = "res_userPhotoDish";
            this.res_userPhotoDish.Size = new System.Drawing.Size(110, 132);
            this.res_userPhotoDish.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.res_userPhotoDish.TabIndex = 172;
            this.res_userPhotoDish.TabStop = false;
            this.res_userPhotoDish.UseTransparentBackground = true;
            // 
            // toolTip_uploadPhoto
            // 
            this.toolTip_uploadPhoto.AllowLinksHandling = true;
            this.toolTip_uploadPhoto.AutoPopDelay = 5000;
            this.toolTip_uploadPhoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_uploadPhoto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_uploadPhoto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_uploadPhoto.InitialDelay = 100;
            this.toolTip_uploadPhoto.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_uploadPhoto.ReshowDelay = 100;
            // 
            // info_dish
            // 
            this.info_dish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.info_dish.Cursor = System.Windows.Forms.Cursors.Default;
            this.info_dish.ForeColor = System.Drawing.Color.AliceBlue;
            this.info_dish.IconChar = FontAwesome.Sharp.IconChar.CircleExclamation;
            this.info_dish.IconColor = System.Drawing.Color.AliceBlue;
            this.info_dish.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.info_dish.IconSize = 28;
            this.info_dish.Location = new System.Drawing.Point(525, 155);
            this.info_dish.Margin = new System.Windows.Forms.Padding(5, 3, 3, 5);
            this.info_dish.Name = "info_dish";
            this.info_dish.Size = new System.Drawing.Size(28, 28);
            this.info_dish.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.info_dish.TabIndex = 193;
            this.info_dish.TabStop = false;
            // 
            // toolTip_infoDish
            // 
            this.toolTip_infoDish.AllowLinksHandling = true;
            this.toolTip_infoDish.AutoPopDelay = 5000;
            this.toolTip_infoDish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_infoDish.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_infoDish.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_infoDish.InitialDelay = 100;
            this.toolTip_infoDish.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_infoDish.ReshowDelay = 100;
            // 
            // toolTip_Products
            // 
            this.toolTip_Products.AllowLinksHandling = true;
            this.toolTip_Products.AutoPopDelay = 5000;
            this.toolTip_Products.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_Products.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_Products.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_Products.InitialDelay = 100;
            this.toolTip_Products.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_Products.ReshowDelay = 100;
            // 
            // closeResults
            // 
            this.closeResults.BackColor = System.Drawing.Color.Transparent;
            this.closeResults.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(13)))), ((int)(((byte)(18)))));
            this.closeResults.BorderRadius = 17;
            this.closeResults.Cursor = System.Windows.Forms.Cursors.Default;
            this.closeResults.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.closeResults.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.closeResults.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.closeResults.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.closeResults.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(13)))), ((int)(((byte)(18)))));
            this.closeResults.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold);
            this.closeResults.ForeColor = System.Drawing.Color.White;
            this.closeResults.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(38)))));
            this.closeResults.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(38)))));
            this.closeResults.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(38)))));
            this.closeResults.Location = new System.Drawing.Point(307, 423);
            this.closeResults.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.closeResults.Name = "closeResults";
            this.closeResults.ShadowDecoration.BorderRadius = 18;
            this.closeResults.ShadowDecoration.Color = System.Drawing.Color.Empty;
            this.closeResults.ShadowDecoration.Depth = 99;
            this.closeResults.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(22);
            this.closeResults.Size = new System.Drawing.Size(101, 40);
            this.closeResults.TabIndex = 191;
            this.closeResults.Text = "Close";
            this.closeResults.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            this.closeResults.UseTransparentBackground = true;
            this.closeResults.Click += new System.EventHandler(this.closeResults_Click);
            // 
            // resultsAnalysis
            // 
            this.resultsAnalysis.BackColor = System.Drawing.Color.Transparent;
            this.resultsAnalysis.BorderRadius = 30;
            this.resultsAnalysis.Cursor = System.Windows.Forms.Cursors.Default;
            this.resultsAnalysis.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(10)))), ((int)(((byte)(93)))));
            this.resultsAnalysis.ImageRotate = 0F;
            this.resultsAnalysis.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.resultsAnalysis.Location = new System.Drawing.Point(158, 31);
            this.resultsAnalysis.Margin = new System.Windows.Forms.Padding(10);
            this.resultsAnalysis.Name = "resultsAnalysis";
            this.resultsAnalysis.Size = new System.Drawing.Size(402, 450);
            this.resultsAnalysis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.resultsAnalysis.TabIndex = 171;
            this.resultsAnalysis.TabStop = false;
            this.resultsAnalysis.UseTransparentBackground = true;
            // 
            // Scanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.info_dish);
            this.Controls.Add(this.res_totalCalories);
            this.Controls.Add(this.closeResults);
            this.Controls.Add(this.res_iron);
            this.Controls.Add(this.res_iron_value);
            this.Controls.Add(this.res_calcium);
            this.Controls.Add(this.res_cholesterol);
            this.Controls.Add(this.res_carbohydrates);
            this.Controls.Add(this.res_fat);
            this.Controls.Add(this.res_protein);
            this.Controls.Add(this.res_cholesterol_value);
            this.Controls.Add(this.res_calcium_value);
            this.Controls.Add(this.res_fat_value);
            this.Controls.Add(this.res_carbohydrates_value);
            this.Controls.Add(this.res_totalCaories_value);
            this.Controls.Add(this.res_protein_value);
            this.Controls.Add(this.res_products);
            this.Controls.Add(this.line);
            this.Controls.Add(this.res_dishName);
            this.Controls.Add(this.res_userPhotoDish);
            this.Controls.Add(this.resultsAnalysis);
            this.Controls.Add(this.deletePhoto);
            this.Controls.Add(this.uploadedUserPoto);
            this.Controls.Add(this.label_Clarifai);
            this.Controls.Add(this.btn_ScanPhoto);
            this.Controls.Add(this.uploadPhotoHover);
            this.Controls.Add(this.homePageImg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Scanner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanner";
            this.Load += new System.EventHandler(this.Scanner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.homePageImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uploadPhotoHover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deletePhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uploadedUserPoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.res_userPhotoDish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.info_dish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsAnalysis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox homePageImg;
        private Guna.UI2.WinForms.Guna2PictureBox uploadPhotoHover;
        private Guna.UI2.WinForms.Guna2Button btn_ScanPhoto;
        private System.Windows.Forms.Label label_Clarifai;
        private Guna.UI2.WinForms.Guna2PictureBox deletePhoto;
        private Guna.UI2.WinForms.Guna2PictureBox uploadedUserPoto;
        private System.Windows.Forms.Label res_totalCalories;
        private System.Windows.Forms.Label res_iron;
        private System.Windows.Forms.Label res_iron_value;
        private System.Windows.Forms.Label res_calcium;
        private System.Windows.Forms.Label res_cholesterol;
        private System.Windows.Forms.Label res_carbohydrates;
        private System.Windows.Forms.Label res_fat;
        private System.Windows.Forms.Label res_protein;
        private System.Windows.Forms.Label res_cholesterol_value;
        private System.Windows.Forms.Label res_calcium_value;
        private System.Windows.Forms.Label res_fat_value;
        private System.Windows.Forms.Label res_carbohydrates_value;
        private System.Windows.Forms.Label res_totalCaories_value;
        private System.Windows.Forms.Label res_protein_value;
        private System.Windows.Forms.Label res_products;
        private Guna.UI2.WinForms.Guna2Panel line;
        private System.Windows.Forms.Label res_dishName;
        private Guna.UI2.WinForms.Guna2PictureBox res_userPhotoDish;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_uploadPhoto;
        private FontAwesome.Sharp.IconPictureBox info_dish;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_infoDish;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_Products;
        private Guna.UI2.WinForms.Guna2Button closeResults;
        private Guna.UI2.WinForms.Guna2PictureBox resultsAnalysis;
    }
}