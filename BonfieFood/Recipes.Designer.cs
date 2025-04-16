namespace BonfieFood
{
    partial class Recipes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recipes));
            this.search = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSearchRecipe = new Guna.UI2.WinForms.Guna2PictureBox();
            this.infoPanel = new Guna.UI2.WinForms.Guna2PictureBox();
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.miniPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.saved_Recipe = new FontAwesome.Sharp.IconPictureBox();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.ingredients = new System.Windows.Forms.Label();
            this.protein = new System.Windows.Forms.Label();
            this.fat = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button11 = new Guna.UI2.WinForms.Guna2Button();
            this.carbo = new System.Windows.Forms.Label();
            this.photoRecipe = new Guna.UI2.WinForms.Guna2PictureBox();
            this.category = new System.Windows.Forms.Label();
            this.calories = new System.Windows.Forms.Label();
            this.nameRecipe = new System.Windows.Forms.Label();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.recipesQuantity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearchRecipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.miniPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saved_Recipe)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photoRecipe)).BeginInit();
            this.SuspendLayout();
            // 
            // search
            // 
            this.search.AcceptsReturn = true;
            this.search.AcceptsTab = true;
            this.search.Animated = true;
            this.search.BackColor = System.Drawing.Color.Transparent;
            this.search.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(163)))), ((int)(((byte)(0)))));
            this.search.BorderRadius = 15;
            this.search.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.search.DefaultText = "";
            this.search.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.search.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.search.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.search.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.search.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.search.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(163)))), ((int)(((byte)(0)))));
            this.search.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.search.ForeColor = System.Drawing.Color.Gainsboro;
            this.search.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(163)))), ((int)(((byte)(0)))));
            this.search.IconRight = global::BonfieFood.Properties.Resources.invisible_pas;
            this.search.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.search.IconRightSize = new System.Drawing.Size(32, 20);
            this.search.Location = new System.Drawing.Point(133, 19);
            this.search.Margin = new System.Windows.Forms.Padding(10, 10, 3, 10);
            this.search.Name = "search";
            this.search.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.search.PlaceholderText = "Search Recipes";
            this.search.SelectedText = "";
            this.search.Size = new System.Drawing.Size(453, 36);
            this.search.TabIndex = 193;
            this.search.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // btnSearchRecipe
            // 
            this.btnSearchRecipe.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchRecipe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchRecipe.FillColor = System.Drawing.Color.OrangeRed;
            this.btnSearchRecipe.Image = global::BonfieFood.Properties.Resources.search_recipes;
            this.btnSearchRecipe.ImageRotate = 0F;
            this.btnSearchRecipe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchRecipe.Location = new System.Drawing.Point(552, 22);
            this.btnSearchRecipe.Name = "btnSearchRecipe";
            this.btnSearchRecipe.Size = new System.Drawing.Size(30, 30);
            this.btnSearchRecipe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSearchRecipe.TabIndex = 194;
            this.btnSearchRecipe.TabStop = false;
            this.btnSearchRecipe.UseTransparentBackground = true;
            this.btnSearchRecipe.Click += new System.EventHandler(this.btnSearchRecipe_Click);
            this.btnSearchRecipe.MouseEnter += new System.EventHandler(this.btnSearchRecipe_MouseEnter);
            this.btnSearchRecipe.MouseLeave += new System.EventHandler(this.btnSearchRecipe_MouseLeave);
            this.btnSearchRecipe.MouseHover += new System.EventHandler(this.btnSearchRecipe_MouseHover);
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.Transparent;
            this.infoPanel.BorderRadius = 30;
            this.infoPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.infoPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.infoPanel.ImageRotate = 0F;
            this.infoPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.infoPanel.Location = new System.Drawing.Point(19, 75);
            this.infoPanel.Margin = new System.Windows.Forms.Padding(10);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(682, 406);
            this.infoPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.infoPanel.TabIndex = 195;
            this.infoPanel.TabStop = false;
            this.infoPanel.UseTransparentBackground = true;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.mainPanel.Controls.Add(this.miniPanel);
            this.mainPanel.Location = new System.Drawing.Point(19, 101);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(682, 355);
            this.mainPanel.TabIndex = 196;
            // 
            // miniPanel
            // 
            this.miniPanel.BackColor = System.Drawing.Color.Transparent;
            this.miniPanel.Controls.Add(this.saved_Recipe);
            this.miniPanel.Controls.Add(this.guna2Panel2);
            this.miniPanel.Controls.Add(this.protein);
            this.miniPanel.Controls.Add(this.fat);
            this.miniPanel.Controls.Add(this.label1);
            this.miniPanel.Controls.Add(this.guna2Button2);
            this.miniPanel.Controls.Add(this.guna2Button1);
            this.miniPanel.Controls.Add(this.guna2Button11);
            this.miniPanel.Controls.Add(this.carbo);
            this.miniPanel.Controls.Add(this.photoRecipe);
            this.miniPanel.Controls.Add(this.category);
            this.miniPanel.Controls.Add(this.calories);
            this.miniPanel.Controls.Add(this.nameRecipe);
            this.miniPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.miniPanel.Location = new System.Drawing.Point(0, 0);
            this.miniPanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.miniPanel.Name = "miniPanel";
            this.miniPanel.Size = new System.Drawing.Size(682, 166);
            this.miniPanel.TabIndex = 0;
            // 
            // saved_Recipe
            // 
            this.saved_Recipe.BackColor = System.Drawing.Color.Transparent;
            this.saved_Recipe.Cursor = System.Windows.Forms.Cursors.Default;
            this.saved_Recipe.ForeColor = System.Drawing.Color.AliceBlue;
            this.saved_Recipe.IconChar = FontAwesome.Sharp.IconChar.Heart;
            this.saved_Recipe.IconColor = System.Drawing.Color.AliceBlue;
            this.saved_Recipe.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.saved_Recipe.IconSize = 28;
            this.saved_Recipe.Location = new System.Drawing.Point(10, 6);
            this.saved_Recipe.Margin = new System.Windows.Forms.Padding(5, 3, 3, 5);
            this.saved_Recipe.Name = "saved_Recipe";
            this.saved_Recipe.Size = new System.Drawing.Size(28, 28);
            this.saved_Recipe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.saved_Recipe.TabIndex = 194;
            this.saved_Recipe.TabStop = false;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.AutoScroll = true;
            this.guna2Panel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel2.Controls.Add(this.ingredients);
            this.guna2Panel2.Location = new System.Drawing.Point(138, 96);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(527, 65);
            this.guna2Panel2.TabIndex = 187;
            // 
            // ingredients
            // 
            this.ingredients.AutoSize = true;
            this.ingredients.BackColor = System.Drawing.Color.Transparent;
            this.ingredients.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ingredients.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ingredients.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ingredients.Location = new System.Drawing.Point(0, 0);
            this.ingredients.Margin = new System.Windows.Forms.Padding(3);
            this.ingredients.MaximumSize = new System.Drawing.Size(512, 0);
            this.ingredients.Name = "ingredients";
            this.ingredients.Size = new System.Drawing.Size(505, 84);
            this.ingredients.TabIndex = 176;
            this.ingredients.Text = resources.GetString("ingredients.Text");
            // 
            // protein
            // 
            this.protein.BackColor = System.Drawing.Color.Transparent;
            this.protein.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.protein.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.protein.Location = new System.Drawing.Point(387, 44);
            this.protein.Margin = new System.Windows.Forms.Padding(15, 5, 3, 0);
            this.protein.Name = "protein";
            this.protein.Size = new System.Drawing.Size(82, 29);
            this.protein.TabIndex = 186;
            this.protein.Text = "999,9 g";
            this.protein.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fat
            // 
            this.fat.BackColor = System.Drawing.Color.Transparent;
            this.fat.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.fat.Location = new System.Drawing.Point(483, 44);
            this.fat.Margin = new System.Windows.Forms.Padding(15, 5, 3, 0);
            this.fat.Name = "fat";
            this.fat.Size = new System.Drawing.Size(82, 29);
            this.fat.TabIndex = 185;
            this.fat.Text = "21,5 g";
            this.fat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Rockwell Extra Bold", 18F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(151)))), ((int)(((byte)(254)))));
            this.label1.Location = new System.Drawing.Point(258, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 28);
            this.label1.TabIndex = 184;
            this.label1.Text = "591,3";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Button2
            // 
            this.guna2Button2.Animated = true;
            this.guna2Button2.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(184)))), ((int)(((byte)(171)))));
            this.guna2Button2.BorderRadius = 5;
            this.guna2Button2.BorderThickness = 2;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(184)))), ((int)(((byte)(171)))));
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.guna2Button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.guna2Button2.HoverState.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.guna2Button2.Location = new System.Drawing.Point(618, 78);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(12, 12);
            this.guna2Button2.TabIndex = 183;
            this.guna2Button2.UseTransparentBackground = true;
            // 
            // guna2Button1
            // 
            this.guna2Button1.Animated = true;
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(175)))), ((int)(((byte)(62)))));
            this.guna2Button1.BorderRadius = 5;
            this.guna2Button1.BorderThickness = 2;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(175)))), ((int)(((byte)(62)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.guna2Button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.guna2Button1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(175)))), ((int)(((byte)(62)))));
            this.guna2Button1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(175)))), ((int)(((byte)(62)))));
            this.guna2Button1.HoverState.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.guna2Button1.Location = new System.Drawing.Point(423, 78);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(12, 12);
            this.guna2Button1.TabIndex = 182;
            this.guna2Button1.UseTransparentBackground = true;
            // 
            // guna2Button11
            // 
            this.guna2Button11.Animated = true;
            this.guna2Button11.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button11.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(116)))), ((int)(((byte)(222)))));
            this.guna2Button11.BorderRadius = 5;
            this.guna2Button11.BorderThickness = 2;
            this.guna2Button11.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button11.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button11.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button11.DisabledState.ForeColor = System.Drawing.Color.DarkGray;
            this.guna2Button11.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(116)))), ((int)(((byte)(222)))));
            this.guna2Button11.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.guna2Button11.ForeColor = System.Drawing.Color.Gainsboro;
            this.guna2Button11.HoverState.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.guna2Button11.Location = new System.Drawing.Point(518, 78);
            this.guna2Button11.Name = "guna2Button11";
            this.guna2Button11.Size = new System.Drawing.Size(12, 12);
            this.guna2Button11.TabIndex = 181;
            this.guna2Button11.UseTransparentBackground = true;
            // 
            // carbo
            // 
            this.carbo.BackColor = System.Drawing.Color.Transparent;
            this.carbo.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.carbo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.carbo.Location = new System.Drawing.Point(583, 44);
            this.carbo.Margin = new System.Windows.Forms.Padding(15, 5, 3, 0);
            this.carbo.Name = "carbo";
            this.carbo.Size = new System.Drawing.Size(82, 29);
            this.carbo.TabIndex = 179;
            this.carbo.Text = "257,6 g";
            this.carbo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // photoRecipe
            // 
            this.photoRecipe.BackColor = System.Drawing.Color.Transparent;
            this.photoRecipe.BorderRadius = 30;
            this.photoRecipe.Cursor = System.Windows.Forms.Cursors.Default;
            this.photoRecipe.FillColor = System.Drawing.Color.Gray;
            this.photoRecipe.ImageRotate = 0F;
            this.photoRecipe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.photoRecipe.Location = new System.Drawing.Point(15, 9);
            this.photoRecipe.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.photoRecipe.Name = "photoRecipe";
            this.photoRecipe.Size = new System.Drawing.Size(100, 94);
            this.photoRecipe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.photoRecipe.TabIndex = 173;
            this.photoRecipe.TabStop = false;
            this.photoRecipe.UseTransparentBackground = true;
            // 
            // category
            // 
            this.category.BackColor = System.Drawing.Color.Transparent;
            this.category.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.category.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.category.Location = new System.Drawing.Point(5, 113);
            this.category.Margin = new System.Windows.Forms.Padding(5, 7, 3, 0);
            this.category.Name = "category";
            this.category.Size = new System.Drawing.Size(121, 48);
            this.category.TabIndex = 131;
            this.category.Text = "petersfoodadventures.com";
            this.category.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calories
            // 
            this.calories.AutoSize = true;
            this.calories.BackColor = System.Drawing.Color.Transparent;
            this.calories.Font = new System.Drawing.Font("Constantia", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.calories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.calories.Location = new System.Drawing.Point(137, 51);
            this.calories.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.calories.Name = "calories";
            this.calories.Size = new System.Drawing.Size(114, 29);
            this.calories.TabIndex = 130;
            this.calories.Text = "Calories:";
            this.calories.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameRecipe
            // 
            this.nameRecipe.BackColor = System.Drawing.Color.Transparent;
            this.nameRecipe.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameRecipe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.nameRecipe.Location = new System.Drawing.Point(137, 9);
            this.nameRecipe.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.nameRecipe.Name = "nameRecipe";
            this.nameRecipe.Size = new System.Drawing.Size(534, 29);
            this.nameRecipe.TabIndex = 128;
            this.nameRecipe.Text = "Soy, Seaweed, and Sesame Potato Salad, Ananas";
            this.nameRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.labelQuantity.Font = new System.Drawing.Font("Constantia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(204)));
            this.labelQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.labelQuantity.Location = new System.Drawing.Point(575, 459);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(58, 18);
            this.labelQuantity.TabIndex = 196;
            this.labelQuantity.Text = "Results:";
            this.labelQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // recipesQuantity
            // 
            this.recipesQuantity.AutoSize = true;
            this.recipesQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.recipesQuantity.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recipesQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(163)))), ((int)(((byte)(0)))));
            this.recipesQuantity.Location = new System.Drawing.Point(630, 457);
            this.recipesQuantity.Name = "recipesQuantity";
            this.recipesQuantity.Size = new System.Drawing.Size(30, 21);
            this.recipesQuantity.TabIndex = 195;
            this.recipesQuantity.Text = "50";
            this.recipesQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Recipes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.recipesQuantity);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.btnSearchRecipe);
            this.Controls.Add(this.search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Recipes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recipes";
            this.Load += new System.EventHandler(this.Recipes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnSearchRecipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.miniPanel.ResumeLayout(false);
            this.miniPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saved_Recipe)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photoRecipe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox search;
        private Guna.UI2.WinForms.Guna2PictureBox btnSearchRecipe;
        private Guna.UI2.WinForms.Guna2PictureBox infoPanel;
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
        private Guna.UI2.WinForms.Guna2Panel miniPanel;
        private System.Windows.Forms.Label category;
        private System.Windows.Forms.Label calories;
        private System.Windows.Forms.Label nameRecipe;
        private Guna.UI2.WinForms.Guna2PictureBox photoRecipe;
        private System.Windows.Forms.Label ingredients;
        private System.Windows.Forms.Label carbo;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button guna2Button11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label protein;
        private System.Windows.Forms.Label fat;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private FontAwesome.Sharp.IconPictureBox saved_Recipe;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.Label recipesQuantity;
    }
}