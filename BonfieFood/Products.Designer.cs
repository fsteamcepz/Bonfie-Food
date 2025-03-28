namespace BonfieFood
{
    partial class Products
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Products));
            this.searchProducts = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSearchProducts = new Guna.UI2.WinForms.Guna2PictureBox();
            this.toolTip_totalFat = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.toolTip_totalCarbohydrates = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.toolTip_Vitamins = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.toolTip_Category = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.toolTip_infoProduct = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.line = new Guna.UI2.WinForms.Guna2Panel();
            this.label_NameProduct = new System.Windows.Forms.Label();
            this.label_Calories = new System.Windows.Forms.Label();
            this.label_Category = new System.Windows.Forms.Label();
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.productQuantity = new System.Windows.Forms.Label();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.sortingName = new FontAwesome.Sharp.IconPictureBox();
            this.sortingCategory = new FontAwesome.Sharp.IconPictureBox();
            this.sortingCalories = new FontAwesome.Sharp.IconPictureBox();
            this.informationMenu = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearchProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortingName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortingCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortingCalories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.informationMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // searchProducts
            // 
            this.searchProducts.AcceptsReturn = true;
            this.searchProducts.AcceptsTab = true;
            this.searchProducts.Animated = true;
            this.searchProducts.BackColor = System.Drawing.Color.Transparent;
            this.searchProducts.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.searchProducts.BorderRadius = 15;
            this.searchProducts.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.searchProducts.DefaultText = "";
            this.searchProducts.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.searchProducts.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.searchProducts.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.searchProducts.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.searchProducts.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.searchProducts.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(107)))), ((int)(((byte)(123)))));
            this.searchProducts.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.searchProducts.ForeColor = System.Drawing.Color.Gainsboro;
            this.searchProducts.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(107)))), ((int)(((byte)(123)))));
            this.searchProducts.IconRight = global::BonfieFood.Properties.Resources.invisible_pas;
            this.searchProducts.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.searchProducts.IconRightSize = new System.Drawing.Size(32, 20);
            this.searchProducts.Location = new System.Drawing.Point(133, 19);
            this.searchProducts.Margin = new System.Windows.Forms.Padding(10, 10, 3, 10);
            this.searchProducts.Name = "searchProducts";
            this.searchProducts.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.searchProducts.PlaceholderText = "Search Products";
            this.searchProducts.SelectedText = "";
            this.searchProducts.Size = new System.Drawing.Size(453, 36);
            this.searchProducts.TabIndex = 95;
            this.searchProducts.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // btnSearchProducts
            // 
            this.btnSearchProducts.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchProducts.FillColor = System.Drawing.Color.OrangeRed;
            this.btnSearchProducts.Image = global::BonfieFood.Properties.Resources.search_product;
            this.btnSearchProducts.ImageRotate = 0F;
            this.btnSearchProducts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchProducts.Location = new System.Drawing.Point(552, 22);
            this.btnSearchProducts.Name = "btnSearchProducts";
            this.btnSearchProducts.Size = new System.Drawing.Size(30, 30);
            this.btnSearchProducts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSearchProducts.TabIndex = 127;
            this.btnSearchProducts.TabStop = false;
            this.btnSearchProducts.UseTransparentBackground = true;
            this.btnSearchProducts.Click += new System.EventHandler(this.btnSearchProducts_Click);
            this.btnSearchProducts.MouseEnter += new System.EventHandler(this.btnSearchProducts_MouseEnter);
            this.btnSearchProducts.MouseLeave += new System.EventHandler(this.btnSearchProducts_MouseLeave);
            this.btnSearchProducts.MouseHover += new System.EventHandler(this.btnSearchProducts_MouseHover);
            // 
            // toolTip_totalFat
            // 
            this.toolTip_totalFat.AllowLinksHandling = true;
            this.toolTip_totalFat.AutoPopDelay = 5000;
            this.toolTip_totalFat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_totalFat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_totalFat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_totalFat.InitialDelay = 100;
            this.toolTip_totalFat.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_totalFat.ReshowDelay = 500;
            // 
            // toolTip_totalCarbohydrates
            // 
            this.toolTip_totalCarbohydrates.AllowLinksHandling = true;
            this.toolTip_totalCarbohydrates.AutoPopDelay = 5000;
            this.toolTip_totalCarbohydrates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_totalCarbohydrates.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_totalCarbohydrates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_totalCarbohydrates.InitialDelay = 100;
            this.toolTip_totalCarbohydrates.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_totalCarbohydrates.ReshowDelay = 500;
            // 
            // toolTip_Vitamins
            // 
            this.toolTip_Vitamins.AllowLinksHandling = true;
            this.toolTip_Vitamins.AutoPopDelay = 5000;
            this.toolTip_Vitamins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_Vitamins.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_Vitamins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_Vitamins.InitialDelay = 100;
            this.toolTip_Vitamins.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_Vitamins.ReshowDelay = 500;
            // 
            // toolTip_Category
            // 
            this.toolTip_Category.AllowLinksHandling = true;
            this.toolTip_Category.AutoPopDelay = 5000;
            this.toolTip_Category.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_Category.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_Category.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_Category.InitialDelay = 100;
            this.toolTip_Category.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_Category.ReshowDelay = 500;
            this.toolTip_Category.TitleForeColor = System.Drawing.Color.DeepSkyBlue;
            // 
            // toolTip_infoProduct
            // 
            this.toolTip_infoProduct.AllowLinksHandling = true;
            this.toolTip_infoProduct.AutoPopDelay = 5000;
            this.toolTip_infoProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_infoProduct.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_infoProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_infoProduct.InitialDelay = 100;
            this.toolTip_infoProduct.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_infoProduct.ReshowDelay = 500;
            // 
            // line
            // 
            this.line.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.line.BackColor = System.Drawing.Color.Silver;
            this.line.Location = new System.Drawing.Point(19, 125);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(682, 1);
            this.line.TabIndex = 117;
            // 
            // label_NameProduct
            // 
            this.label_NameProduct.AutoSize = true;
            this.label_NameProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.label_NameProduct.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Bold);
            this.label_NameProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_NameProduct.Location = new System.Drawing.Point(46, 86);
            this.label_NameProduct.Name = "label_NameProduct";
            this.label_NameProduct.Size = new System.Drawing.Size(79, 29);
            this.label_NameProduct.TabIndex = 118;
            this.label_NameProduct.Text = "Name";
            this.label_NameProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Calories
            // 
            this.label_Calories.AutoSize = true;
            this.label_Calories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.label_Calories.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Calories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_Calories.Location = new System.Drawing.Point(555, 86);
            this.label_Calories.Name = "label_Calories";
            this.label_Calories.Size = new System.Drawing.Size(106, 29);
            this.label_Calories.TabIndex = 119;
            this.label_Calories.Text = "Calories";
            this.label_Calories.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Category
            // 
            this.label_Category.AutoSize = true;
            this.label_Category.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.label_Category.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Bold);
            this.label_Category.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_Category.Location = new System.Drawing.Point(368, 86);
            this.label_Category.Name = "label_Category";
            this.label_Category.Size = new System.Drawing.Size(112, 29);
            this.label_Category.TabIndex = 120;
            this.label_Category.Text = "Category";
            this.label_Category.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.mainPanel.Location = new System.Drawing.Point(19, 132);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(682, 321);
            this.mainPanel.TabIndex = 126;
            // 
            // productQuantity
            // 
            this.productQuantity.AutoSize = true;
            this.productQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.productQuantity.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.productQuantity.ForeColor = System.Drawing.Color.Tomato;
            this.productQuantity.Location = new System.Drawing.Point(630, 457);
            this.productQuantity.Name = "productQuantity";
            this.productQuantity.Size = new System.Drawing.Size(30, 21);
            this.productQuantity.TabIndex = 132;
            this.productQuantity.Text = "50";
            this.productQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.labelQuantity.Font = new System.Drawing.Font("Constantia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(204)));
            this.labelQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.labelQuantity.Location = new System.Drawing.Point(575, 459);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(58, 18);
            this.labelQuantity.TabIndex = 133;
            this.labelQuantity.Text = "Results:";
            this.labelQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sortingName
            // 
            this.sortingName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.sortingName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sortingName.ForeColor = System.Drawing.Color.AliceBlue;
            this.sortingName.IconChar = FontAwesome.Sharp.IconChar.AngleDown;
            this.sortingName.IconColor = System.Drawing.Color.AliceBlue;
            this.sortingName.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.sortingName.IconSize = 22;
            this.sortingName.Location = new System.Drawing.Point(122, 93);
            this.sortingName.Margin = new System.Windows.Forms.Padding(5, 3, 3, 5);
            this.sortingName.Name = "sortingName";
            this.sortingName.Size = new System.Drawing.Size(22, 22);
            this.sortingName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sortingName.TabIndex = 204;
            this.sortingName.TabStop = false;
            this.sortingName.Click += new System.EventHandler(this.sortingName_Click);
            // 
            // sortingCategory
            // 
            this.sortingCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.sortingCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sortingCategory.ForeColor = System.Drawing.Color.AliceBlue;
            this.sortingCategory.IconChar = FontAwesome.Sharp.IconChar.AngleDown;
            this.sortingCategory.IconColor = System.Drawing.Color.AliceBlue;
            this.sortingCategory.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.sortingCategory.IconSize = 22;
            this.sortingCategory.Location = new System.Drawing.Point(478, 93);
            this.sortingCategory.Margin = new System.Windows.Forms.Padding(5, 3, 3, 5);
            this.sortingCategory.Name = "sortingCategory";
            this.sortingCategory.Size = new System.Drawing.Size(22, 22);
            this.sortingCategory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sortingCategory.TabIndex = 205;
            this.sortingCategory.TabStop = false;
            this.sortingCategory.Click += new System.EventHandler(this.sortingCategory_Click);
            // 
            // sortingCalories
            // 
            this.sortingCalories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.sortingCalories.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sortingCalories.ForeColor = System.Drawing.Color.AliceBlue;
            this.sortingCalories.IconChar = FontAwesome.Sharp.IconChar.AngleDown;
            this.sortingCalories.IconColor = System.Drawing.Color.AliceBlue;
            this.sortingCalories.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.sortingCalories.IconSize = 22;
            this.sortingCalories.Location = new System.Drawing.Point(659, 93);
            this.sortingCalories.Margin = new System.Windows.Forms.Padding(5, 3, 3, 5);
            this.sortingCalories.Name = "sortingCalories";
            this.sortingCalories.Size = new System.Drawing.Size(22, 22);
            this.sortingCalories.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sortingCalories.TabIndex = 206;
            this.sortingCalories.TabStop = false;
            this.sortingCalories.Click += new System.EventHandler(this.sortingCalories_Click);
            // 
            // informationMenu
            // 
            this.informationMenu.BackColor = System.Drawing.Color.Transparent;
            this.informationMenu.BorderRadius = 30;
            this.informationMenu.Cursor = System.Windows.Forms.Cursors.Default;
            this.informationMenu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(193)))));
            this.informationMenu.ImageRotate = 0F;
            this.informationMenu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.informationMenu.Location = new System.Drawing.Point(19, 75);
            this.informationMenu.Margin = new System.Windows.Forms.Padding(10);
            this.informationMenu.Name = "informationMenu";
            this.informationMenu.Size = new System.Drawing.Size(682, 406);
            this.informationMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.informationMenu.TabIndex = 91;
            this.informationMenu.TabStop = false;
            this.informationMenu.UseTransparentBackground = true;
            // 
            // Products
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(46)))));
            this.BackgroundImage = global::BonfieFood.Properties.Resources.products_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.sortingCalories);
            this.Controls.Add(this.sortingCategory);
            this.Controls.Add(this.sortingName);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.productQuantity);
            this.Controls.Add(this.btnSearchProducts);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.label_Category);
            this.Controls.Add(this.label_Calories);
            this.Controls.Add(this.label_NameProduct);
            this.Controls.Add(this.line);
            this.Controls.Add(this.searchProducts);
            this.Controls.Add(this.informationMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Products";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Products";
            this.Load += new System.EventHandler(this.Products_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnSearchProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortingName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortingCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortingCalories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.informationMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox searchProducts;
        private Guna.UI2.WinForms.Guna2PictureBox btnSearchProducts;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_totalFat;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_totalCarbohydrates;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_Vitamins;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_Category;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_infoProduct;
        private Guna.UI2.WinForms.Guna2Panel line;
        private System.Windows.Forms.Label label_NameProduct;
        private System.Windows.Forms.Label label_Calories;
        private System.Windows.Forms.Label label_Category;
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
        private System.Windows.Forms.Label productQuantity;
        private System.Windows.Forms.Label labelQuantity;
        private FontAwesome.Sharp.IconPictureBox sortingName;
        private FontAwesome.Sharp.IconPictureBox sortingCategory;
        private FontAwesome.Sharp.IconPictureBox sortingCalories;
        private Guna.UI2.WinForms.Guna2PictureBox informationMenu;
    }
}