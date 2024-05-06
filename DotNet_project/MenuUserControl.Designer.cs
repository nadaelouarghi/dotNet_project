namespace DotNet_project
{
    partial class MenuUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuUserControl));
            this.menupregroupBox = new System.Windows.Forms.GroupBox();
            this.orderlabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.orderIcon = new System.Windows.Forms.PictureBox();
            this.logoIcon = new System.Windows.Forms.PictureBox();
            this.accountlabel = new System.Windows.Forms.Label();
            this.accountIcon = new System.Windows.Forms.PictureBox();
            this.monpanierlabel = new System.Windows.Forms.Label();
            this.welcomemsglabel = new System.Windows.Forms.Label();
            this.cartIcon = new System.Windows.Forms.PictureBox();
            this.menupregroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // menupregroupBox
            // 
            this.menupregroupBox.BackColor = System.Drawing.Color.Linen;
            this.menupregroupBox.Controls.Add(this.orderlabel);
            this.menupregroupBox.Controls.Add(this.label1);
            this.menupregroupBox.Controls.Add(this.orderIcon);
            this.menupregroupBox.Controls.Add(this.logoIcon);
            this.menupregroupBox.Controls.Add(this.accountlabel);
            this.menupregroupBox.Controls.Add(this.accountIcon);
            this.menupregroupBox.Controls.Add(this.monpanierlabel);
            this.menupregroupBox.Controls.Add(this.welcomemsglabel);
            this.menupregroupBox.Controls.Add(this.cartIcon);
            this.menupregroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menupregroupBox.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.menupregroupBox.Location = new System.Drawing.Point(3, 0);
            this.menupregroupBox.Name = "menupregroupBox";
            this.menupregroupBox.Size = new System.Drawing.Size(1080, 88);
            this.menupregroupBox.TabIndex = 16;
            this.menupregroupBox.TabStop = false;
            // 
            // orderlabel
            // 
            this.orderlabel.AutoSize = true;
            this.orderlabel.ForeColor = System.Drawing.Color.Black;
            this.orderlabel.Location = new System.Drawing.Point(658, 64);
            this.orderlabel.Name = "orderlabel";
            this.orderlabel.Size = new System.Drawing.Size(148, 20);
            this.orderlabel.TabIndex = 23;
            this.orderlabel.Text = "mes commandes ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Your E Shop";
            // 
            // orderIcon
            // 
            this.orderIcon.Image = ((System.Drawing.Image)(resources.GetObject("orderIcon.Image")));
            this.orderIcon.Location = new System.Drawing.Point(715, 20);
            this.orderIcon.Name = "orderIcon";
            this.orderIcon.Size = new System.Drawing.Size(43, 41);
            this.orderIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.orderIcon.TabIndex = 22;
            this.orderIcon.TabStop = false;
            this.orderIcon.Click += new System.EventHandler(this.orderIcon_Click);
            // 
            // logoIcon
            // 
            this.logoIcon.Image = ((System.Drawing.Image)(resources.GetObject("logoIcon.Image")));
            this.logoIcon.Location = new System.Drawing.Point(11, 20);
            this.logoIcon.Name = "logoIcon";
            this.logoIcon.Size = new System.Drawing.Size(63, 57);
            this.logoIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoIcon.TabIndex = 17;
            this.logoIcon.TabStop = false;
            this.logoIcon.Click += new System.EventHandler(this.logoIcon_Click);
            // 
            // accountlabel
            // 
            this.accountlabel.AutoSize = true;
            this.accountlabel.ForeColor = System.Drawing.Color.Black;
            this.accountlabel.Location = new System.Drawing.Point(812, 64);
            this.accountlabel.Name = "accountlabel";
            this.accountlabel.Size = new System.Drawing.Size(107, 20);
            this.accountlabel.TabIndex = 21;
            this.accountlabel.Text = "mon compte";
            // 
            // accountIcon
            // 
            this.accountIcon.Image = ((System.Drawing.Image)(resources.GetObject("accountIcon.Image")));
            this.accountIcon.Location = new System.Drawing.Point(846, 20);
            this.accountIcon.Name = "accountIcon";
            this.accountIcon.Size = new System.Drawing.Size(43, 41);
            this.accountIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.accountIcon.TabIndex = 19;
            this.accountIcon.TabStop = false;
            this.accountIcon.Click += new System.EventHandler(this.accountIcon_Click);
            // 
            // monpanierlabel
            // 
            this.monpanierlabel.AutoSize = true;
            this.monpanierlabel.ForeColor = System.Drawing.Color.Black;
            this.monpanierlabel.Location = new System.Drawing.Point(942, 65);
            this.monpanierlabel.Name = "monpanierlabel";
            this.monpanierlabel.Size = new System.Drawing.Size(98, 20);
            this.monpanierlabel.TabIndex = 14;
            this.monpanierlabel.Text = "mon panier";
            // 
            // welcomemsglabel
            // 
            this.welcomemsglabel.AutoSize = true;
            this.welcomemsglabel.Location = new System.Drawing.Point(264, 41);
            this.welcomemsglabel.Name = "welcomemsglabel";
            this.welcomemsglabel.Size = new System.Drawing.Size(0, 20);
            this.welcomemsglabel.TabIndex = 12;
            // 
            // cartIcon
            // 
            this.cartIcon.Image = ((System.Drawing.Image)(resources.GetObject("cartIcon.Image")));
            this.cartIcon.Location = new System.Drawing.Point(965, 20);
            this.cartIcon.Name = "cartIcon";
            this.cartIcon.Size = new System.Drawing.Size(43, 41);
            this.cartIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cartIcon.TabIndex = 13;
            this.cartIcon.TabStop = false;
            this.cartIcon.Click += new System.EventHandler(this.cartIcon_Click);
            // 
            // MenuUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menupregroupBox);
            this.Name = "MenuUserControl";
            this.Size = new System.Drawing.Size(1080, 88);
            this.menupregroupBox.ResumeLayout(false);
            this.menupregroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cartIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox menupregroupBox;
        private System.Windows.Forms.Label orderlabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label accountlabel;
        private System.Windows.Forms.Label monpanierlabel;
        private System.Windows.Forms.Label welcomemsglabel;
        public System.Windows.Forms.PictureBox cartIcon;
        public System.Windows.Forms.PictureBox orderIcon;
        public System.Windows.Forms.PictureBox accountIcon;
        public System.Windows.Forms.PictureBox logoIcon;
    }
}
