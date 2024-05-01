namespace DotNet_project
{
    partial class cart
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
            this.cart_menuUserControl = new DotNet_project.MenuUserControl();
            this.SuspendLayout();
            // 
            // cart_menuUserControl
            // 
            this.cart_menuUserControl.Location = new System.Drawing.Point(-3, -4);
            this.cart_menuUserControl.Name = "cart_menuUserControl";
            this.cart_menuUserControl.Size = new System.Drawing.Size(1080, 88);
            this.cart_menuUserControl.TabIndex = 0;
            // 
            // cart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1078, 844);
            this.Controls.Add(this.cart_menuUserControl);
            this.Name = "cart";
            this.Text = "Mon panier";
            this.Load += new System.EventHandler(this.cart_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MenuUserControl cart_menuUserControl;
    }
}