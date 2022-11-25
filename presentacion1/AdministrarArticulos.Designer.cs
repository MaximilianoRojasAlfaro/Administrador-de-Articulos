namespace presentacion1
{
    partial class frmAdminArticulos
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAdministrar = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFiltrar = new System.Windows.Forms.Label();
            this.lblCampo = new System.Windows.Forms.Label();
            this.lblCriterio = new System.Windows.Forms.Label();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.tbxFiltro = new System.Windows.Forms.TextBox();
            this.cbxCriterio = new System.Windows.Forms.ComboBox();
            this.cbxCampo = new System.Windows.Forms.ComboBox();
            this.btnQuitarFiltro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAdministrar
            // 
            this.lblAdministrar.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdministrar.Location = new System.Drawing.Point(50, 394);
            this.lblAdministrar.Name = "lblAdministrar";
            this.lblAdministrar.Size = new System.Drawing.Size(191, 29);
            this.lblAdministrar.TabIndex = 7;
            this.lblAdministrar.Text = "Agregar Articulo";
            this.lblAdministrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAdministrar.UseMnemonic = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(78, 439);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(125, 37);
            this.btnAgregar.TabIndex = 4;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(303, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(597, 514);
            this.panel1.TabIndex = 12;
            // 
            // lblFiltrar
            // 
            this.lblFiltrar.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltrar.Location = new System.Drawing.Point(81, 12);
            this.lblFiltrar.Name = "lblFiltrar";
            this.lblFiltrar.Size = new System.Drawing.Size(111, 29);
            this.lblFiltrar.TabIndex = 13;
            this.lblFiltrar.Text = "Filtrar";
            this.lblFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFiltrar.UseMnemonic = false;
            // 
            // lblCampo
            // 
            this.lblCampo.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCampo.Location = new System.Drawing.Point(22, 59);
            this.lblCampo.Name = "lblCampo";
            this.lblCampo.Size = new System.Drawing.Size(68, 29);
            this.lblCampo.TabIndex = 14;
            this.lblCampo.Text = "Campo:";
            this.lblCampo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCampo.UseMnemonic = false;
            // 
            // lblCriterio
            // 
            this.lblCriterio.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriterio.Location = new System.Drawing.Point(21, 109);
            this.lblCriterio.Name = "lblCriterio";
            this.lblCriterio.Size = new System.Drawing.Size(69, 29);
            this.lblCriterio.TabIndex = 15;
            this.lblCriterio.Text = "Criterio:";
            this.lblCriterio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCriterio.UseMnemonic = false;
            // 
            // lblFiltro
            // 
            this.lblFiltro.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltro.Location = new System.Drawing.Point(22, 155);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(68, 29);
            this.lblFiltro.TabIndex = 16;
            this.lblFiltro.Text = "Filtro:";
            this.lblFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFiltro.UseMnemonic = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(86, 201);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(116, 37);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // tbxFiltro
            // 
            this.tbxFiltro.Location = new System.Drawing.Point(86, 161);
            this.tbxFiltro.Name = "tbxFiltro";
            this.tbxFiltro.Size = new System.Drawing.Size(142, 20);
            this.tbxFiltro.TabIndex = 2;
            this.tbxFiltro.TextChanged += new System.EventHandler(this.tbxFiltro_TextChanged);
            // 
            // cbxCriterio
            // 
            this.cbxCriterio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCriterio.FormattingEnabled = true;
            this.cbxCriterio.Location = new System.Drawing.Point(86, 115);
            this.cbxCriterio.Name = "cbxCriterio";
            this.cbxCriterio.Size = new System.Drawing.Size(142, 21);
            this.cbxCriterio.TabIndex = 1;
            this.cbxCriterio.SelectedIndexChanged += new System.EventHandler(this.cbxCriterio_SelectedIndexChanged);
            // 
            // cbxCampo
            // 
            this.cbxCampo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCampo.FormattingEnabled = true;
            this.cbxCampo.Location = new System.Drawing.Point(86, 67);
            this.cbxCampo.Name = "cbxCampo";
            this.cbxCampo.Size = new System.Drawing.Size(142, 21);
            this.cbxCampo.TabIndex = 0;
            this.cbxCampo.SelectedIndexChanged += new System.EventHandler(this.cbxCampo_SelectedIndexChanged);
            // 
            // btnQuitarFiltro
            // 
            this.btnQuitarFiltro.Location = new System.Drawing.Point(206, 280);
            this.btnQuitarFiltro.Name = "btnQuitarFiltro";
            this.btnQuitarFiltro.Size = new System.Drawing.Size(94, 47);
            this.btnQuitarFiltro.TabIndex = 17;
            this.btnQuitarFiltro.Text = "Quitar Filtro";
            this.btnQuitarFiltro.UseVisualStyleBackColor = true;
            this.btnQuitarFiltro.Visible = false;
            this.btnQuitarFiltro.Click += new System.EventHandler(this.btnQuitarFiltro_Click);
            // 
            // frmAdminArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 546);
            this.Controls.Add(this.btnQuitarFiltro);
            this.Controls.Add(this.lblAdministrar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.cbxCampo);
            this.Controls.Add(this.cbxCriterio);
            this.Controls.Add(this.tbxFiltro);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblFiltro);
            this.Controls.Add(this.lblCriterio);
            this.Controls.Add(this.lblCampo);
            this.Controls.Add(this.lblFiltrar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAdminArticulos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrar Artículos";
            this.Load += new System.EventHandler(this.frmAdminArticulos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblAdministrar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFiltrar;
        private System.Windows.Forms.Label lblCampo;
        private System.Windows.Forms.Label lblCriterio;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox tbxFiltro;
        private System.Windows.Forms.ComboBox cbxCriterio;
        private System.Windows.Forms.ComboBox cbxCampo;
        private System.Windows.Forms.Button btnQuitarFiltro;
    }
}

