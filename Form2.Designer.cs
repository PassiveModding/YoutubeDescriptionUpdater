namespace YoutubeDescriptionReplacer
{
    partial class Form2
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.adflyUserId = new System.Windows.Forms.TextBox();
            this.adflyApiKey = new System.Windows.Forms.TextBox();
            this.AdflyResolve = new System.Windows.Forms.Button();
            this.Domains = new System.Windows.Forms.TextBox();
            this.ResolveDomains = new System.Windows.Forms.Button();
            this.ProgressText = new System.Windows.Forms.Label();
            this.UpdateProgress = new System.Windows.Forms.ProgressBar();
            this.nextIndex = new System.Windows.Forms.Button();
            this.prevIndex = new System.Windows.Forms.Button();
            this.HistoryView = new System.Windows.Forms.TextBox();
            this.IgnoreCaseSearch = new System.Windows.Forms.CheckBox();
            this.VideoSearchText = new System.Windows.Forms.TextBox();
            this.SearchVideosButton = new System.Windows.Forms.Button();
            this.TransformationClear = new System.Windows.Forms.Button();
            this.SelectSecrets = new System.Windows.Forms.Button();
            this.TestUpdate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TransformationView = new System.Windows.Forms.TextBox();
            this.VideoInformation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IgnoreCaseCheck = new System.Windows.Forms.CheckBox();
            this.DescriptionUpdateOptions = new System.Windows.Forms.ComboBox();
            this.UpdateDescriptions = new System.Windows.Forms.Button();
            this.MethodAddButton = new System.Windows.Forms.Button();
            this.GetVideos = new System.Windows.Forms.Button();
            this.LoadBackup = new System.Windows.Forms.Button();
            this.LoadTransforms = new System.Windows.Forms.Button();
            this.SecondaryValueBox = new SpellBox();
            this.PrimaryValueBox = new SpellBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1258, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 26);
            this.label5.TabIndex = 65;
            this.label5.Text = "Asf.ly Unshortener\r\n(may need to be run multiple times)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1258, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 26);
            this.label4.TabIndex = 64;
            this.label4.Text = "Link Unshortener \r\n(input domain name only on individual lines)";
            // 
            // adflyUserId
            // 
            this.adflyUserId.Location = new System.Drawing.Point(1258, 293);
            this.adflyUserId.Name = "adflyUserId";
            this.adflyUserId.Size = new System.Drawing.Size(243, 20);
            this.adflyUserId.TabIndex = 61;
            this.adflyUserId.Text = "Adf.ly user id";
            // 
            // adflyApiKey
            // 
            this.adflyApiKey.Location = new System.Drawing.Point(1258, 267);
            this.adflyApiKey.Name = "adflyApiKey";
            this.adflyApiKey.Size = new System.Drawing.Size(243, 20);
            this.adflyApiKey.TabIndex = 60;
            this.adflyApiKey.Text = "Adf.ly Api Key";
            // 
            // AdflyResolve
            // 
            this.AdflyResolve.Enabled = false;
            this.AdflyResolve.Location = new System.Drawing.Point(1258, 319);
            this.AdflyResolve.Name = "AdflyResolve";
            this.AdflyResolve.Size = new System.Drawing.Size(243, 34);
            this.AdflyResolve.TabIndex = 59;
            this.AdflyResolve.Text = "Resolve (note only resolves links relating to the adf.ly account in question)";
            this.AdflyResolve.UseVisualStyleBackColor = true;
            this.AdflyResolve.Click += new System.EventHandler(this.AdflyResolve_Click);
            // 
            // Domains
            // 
            this.Domains.Location = new System.Drawing.Point(1258, 41);
            this.Domains.Multiline = true;
            this.Domains.Name = "Domains";
            this.Domains.Size = new System.Drawing.Size(243, 154);
            this.Domains.TabIndex = 58;
            this.Domains.Text = "goo.gl\r\nbit.ly";
            // 
            // ResolveDomains
            // 
            this.ResolveDomains.Enabled = false;
            this.ResolveDomains.Location = new System.Drawing.Point(1258, 201);
            this.ResolveDomains.Name = "ResolveDomains";
            this.ResolveDomains.Size = new System.Drawing.Size(243, 34);
            this.ResolveDomains.TabIndex = 57;
            this.ResolveDomains.Text = "Resolve (note will not resolve if there is an intermittent domain)";
            this.ResolveDomains.UseVisualStyleBackColor = true;
            this.ResolveDomains.Click += new System.EventHandler(this.ResolveDomains_Click);
            // 
            // ProgressText
            // 
            this.ProgressText.AutoSize = true;
            this.ProgressText.BackColor = System.Drawing.Color.Transparent;
            this.ProgressText.Location = new System.Drawing.Point(16, 421);
            this.ProgressText.MinimumSize = new System.Drawing.Size(420, 13);
            this.ProgressText.Name = "ProgressText";
            this.ProgressText.Size = new System.Drawing.Size(420, 13);
            this.ProgressText.TabIndex = 56;
            this.ProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UpdateProgress
            // 
            this.UpdateProgress.Location = new System.Drawing.Point(13, 417);
            this.UpdateProgress.Name = "UpdateProgress";
            this.UpdateProgress.Size = new System.Drawing.Size(427, 21);
            this.UpdateProgress.TabIndex = 55;
            // 
            // nextIndex
            // 
            this.nextIndex.Enabled = false;
            this.nextIndex.Location = new System.Drawing.Point(1228, 645);
            this.nextIndex.Name = "nextIndex";
            this.nextIndex.Size = new System.Drawing.Size(24, 24);
            this.nextIndex.TabIndex = 54;
            this.nextIndex.Text = ">";
            this.nextIndex.UseVisualStyleBackColor = true;
            this.nextIndex.Click += new System.EventHandler(this.nextIndex_Click);
            // 
            // prevIndex
            // 
            this.prevIndex.Enabled = false;
            this.prevIndex.Location = new System.Drawing.Point(1198, 645);
            this.prevIndex.Name = "prevIndex";
            this.prevIndex.Size = new System.Drawing.Size(24, 24);
            this.prevIndex.TabIndex = 53;
            this.prevIndex.Text = "<";
            this.prevIndex.UseVisualStyleBackColor = true;
            this.prevIndex.Click += new System.EventHandler(this.prevIndex_Click);
            // 
            // HistoryView
            // 
            this.HistoryView.Location = new System.Drawing.Point(12, 444);
            this.HistoryView.Multiline = true;
            this.HistoryView.Name = "HistoryView";
            this.HistoryView.ReadOnly = true;
            this.HistoryView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.HistoryView.Size = new System.Drawing.Size(826, 225);
            this.HistoryView.TabIndex = 52;
            // 
            // IgnoreCaseSearch
            // 
            this.IgnoreCaseSearch.AutoSize = true;
            this.IgnoreCaseSearch.Location = new System.Drawing.Point(1109, 652);
            this.IgnoreCaseSearch.Name = "IgnoreCaseSearch";
            this.IgnoreCaseSearch.Size = new System.Drawing.Size(83, 17);
            this.IgnoreCaseSearch.TabIndex = 51;
            this.IgnoreCaseSearch.Text = "Ignore Case";
            this.IgnoreCaseSearch.UseVisualStyleBackColor = true;
            // 
            // VideoSearchText
            // 
            this.VideoSearchText.Location = new System.Drawing.Point(844, 536);
            this.VideoSearchText.Multiline = true;
            this.VideoSearchText.Name = "VideoSearchText";
            this.VideoSearchText.Size = new System.Drawing.Size(408, 103);
            this.VideoSearchText.TabIndex = 50;
            // 
            // SearchVideosButton
            // 
            this.SearchVideosButton.Enabled = false;
            this.SearchVideosButton.Location = new System.Drawing.Point(844, 645);
            this.SearchVideosButton.Name = "SearchVideosButton";
            this.SearchVideosButton.Size = new System.Drawing.Size(259, 24);
            this.SearchVideosButton.TabIndex = 49;
            this.SearchVideosButton.Text = "Search Videos";
            this.SearchVideosButton.UseVisualStyleBackColor = true;
            this.SearchVideosButton.Click += new System.EventHandler(this.SearchVideosButton_Click);
            // 
            // TransformationClear
            // 
            this.TransformationClear.Location = new System.Drawing.Point(391, 319);
            this.TransformationClear.Name = "TransformationClear";
            this.TransformationClear.Size = new System.Drawing.Size(48, 34);
            this.TransformationClear.TabIndex = 48;
            this.TransformationClear.Text = "Clear";
            this.TransformationClear.UseVisualStyleBackColor = true;
            this.TransformationClear.Click += new System.EventHandler(this.TransformationClear_Click);
            // 
            // SelectSecrets
            // 
            this.SelectSecrets.Location = new System.Drawing.Point(12, 12);
            this.SelectSecrets.Name = "SelectSecrets";
            this.SelectSecrets.Size = new System.Drawing.Size(427, 36);
            this.SelectSecrets.TabIndex = 47;
            this.SelectSecrets.Text = "Select Secrets File";
            this.SelectSecrets.UseVisualStyleBackColor = true;
            this.SelectSecrets.Click += new System.EventHandler(this.SelectSecrets_Click);
            // 
            // TestUpdate
            // 
            this.TestUpdate.Enabled = false;
            this.TestUpdate.Location = new System.Drawing.Point(12, 359);
            this.TestUpdate.Name = "TestUpdate";
            this.TestUpdate.Size = new System.Drawing.Size(427, 23);
            this.TestUpdate.TabIndex = 46;
            this.TestUpdate.Text = "Update Descriptions";
            this.TestUpdate.UseVisualStyleBackColor = true;
            this.TestUpdate.Click += new System.EventHandler(this.TestUpdate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Transformations";
            // 
            // TransformationView
            // 
            this.TransformationView.Location = new System.Drawing.Point(445, 12);
            this.TransformationView.Multiline = true;
            this.TransformationView.Name = "TransformationView";
            this.TransformationView.ReadOnly = true;
            this.TransformationView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TransformationView.Size = new System.Drawing.Size(393, 426);
            this.TransformationView.TabIndex = 44;
            this.TransformationView.Text = "Transformations:";
            // 
            // VideoInformation
            // 
            this.VideoInformation.Location = new System.Drawing.Point(844, 12);
            this.VideoInformation.Multiline = true;
            this.VideoInformation.Name = "VideoInformation";
            this.VideoInformation.ReadOnly = true;
            this.VideoInformation.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.VideoInformation.Size = new System.Drawing.Size(408, 518);
            this.VideoInformation.TabIndex = 43;
            this.VideoInformation.Text = "Video Information:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Secondary Value (content used in replace)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Primary Value (content searched for)";
            // 
            // IgnoreCaseCheck
            // 
            this.IgnoreCaseCheck.AutoSize = true;
            this.IgnoreCaseCheck.Enabled = false;
            this.IgnoreCaseCheck.Location = new System.Drawing.Point(351, 120);
            this.IgnoreCaseCheck.Name = "IgnoreCaseCheck";
            this.IgnoreCaseCheck.Size = new System.Drawing.Size(83, 17);
            this.IgnoreCaseCheck.TabIndex = 40;
            this.IgnoreCaseCheck.Text = "Ignore Case";
            this.IgnoreCaseCheck.UseVisualStyleBackColor = true;
            // 
            // DescriptionUpdateOptions
            // 
            this.DescriptionUpdateOptions.FormattingEnabled = true;
            this.DescriptionUpdateOptions.Items.AddRange(new object[] {
            "Append",
            "Prepend",
            "Replace",
            "Remove"});
            this.DescriptionUpdateOptions.Location = new System.Drawing.Point(12, 116);
            this.DescriptionUpdateOptions.MaxDropDownItems = 4;
            this.DescriptionUpdateOptions.Name = "DescriptionUpdateOptions";
            this.DescriptionUpdateOptions.Size = new System.Drawing.Size(333, 21);
            this.DescriptionUpdateOptions.TabIndex = 39;
            this.DescriptionUpdateOptions.Text = "Update Method";
            this.DescriptionUpdateOptions.SelectedIndexChanged += new System.EventHandler(this.DescriptionUpdateOptions_SelectedIndexChanged);
            // 
            // UpdateDescriptions
            // 
            this.UpdateDescriptions.Enabled = false;
            this.UpdateDescriptions.Location = new System.Drawing.Point(13, 388);
            this.UpdateDescriptions.Name = "UpdateDescriptions";
            this.UpdateDescriptions.Size = new System.Drawing.Size(427, 23);
            this.UpdateDescriptions.TabIndex = 38;
            this.UpdateDescriptions.Text = "Upload X Changes";
            this.UpdateDescriptions.UseVisualStyleBackColor = true;
            this.UpdateDescriptions.Click += new System.EventHandler(this.UpdateDescriptions_Click);
            // 
            // MethodAddButton
            // 
            this.MethodAddButton.Enabled = false;
            this.MethodAddButton.Location = new System.Drawing.Point(13, 319);
            this.MethodAddButton.Name = "MethodAddButton";
            this.MethodAddButton.Size = new System.Drawing.Size(372, 34);
            this.MethodAddButton.TabIndex = 37;
            this.MethodAddButton.Text = "Add";
            this.MethodAddButton.UseVisualStyleBackColor = true;
            this.MethodAddButton.Click += new System.EventHandler(this.MethodAddButton_Click);
            // 
            // GetVideos
            // 
            this.GetVideos.Enabled = false;
            this.GetVideos.Location = new System.Drawing.Point(13, 54);
            this.GetVideos.Name = "GetVideos";
            this.GetVideos.Size = new System.Drawing.Size(427, 36);
            this.GetVideos.TabIndex = 36;
            this.GetVideos.Text = "Get My Videos";
            this.GetVideos.UseVisualStyleBackColor = true;
            this.GetVideos.Click += new System.EventHandler(this.GetVideos_ClickAsync);
            // 
            // LoadBackup
            // 
            this.LoadBackup.Enabled = false;
            this.LoadBackup.Location = new System.Drawing.Point(1261, 603);
            this.LoadBackup.Name = "LoadBackup";
            this.LoadBackup.Size = new System.Drawing.Size(243, 36);
            this.LoadBackup.TabIndex = 66;
            this.LoadBackup.Text = "Get Videos from Backup";
            this.LoadBackup.UseVisualStyleBackColor = true;
            this.LoadBackup.Click += new System.EventHandler(this.LoadBackup_Click);
            // 
            // LoadTransforms
            // 
            this.LoadTransforms.Enabled = false;
            this.LoadTransforms.Location = new System.Drawing.Point(1261, 563);
            this.LoadTransforms.Name = "LoadTransforms";
            this.LoadTransforms.Size = new System.Drawing.Size(243, 36);
            this.LoadTransforms.TabIndex = 67;
            this.LoadTransforms.Text = "Get Transformations from File";
            this.LoadTransforms.UseVisualStyleBackColor = true;
            this.LoadTransforms.Click += new System.EventHandler(this.LoadTransforms_Click);
            // 
            // SecondaryValueBox
            // 
            this.SecondaryValueBox.Enabled = false;
            this.SecondaryValueBox.Location = new System.Drawing.Point(229, 156);
            this.SecondaryValueBox.Multiline = true;
            this.SecondaryValueBox.Name = "SecondaryValueBox";
            this.SecondaryValueBox.Size = new System.Drawing.Size(210, 154);
            this.SecondaryValueBox.TabIndex = 63;
            this.SecondaryValueBox.Child = new System.Windows.Controls.TextBox();
            // 
            // PrimaryValueBox
            // 
            this.PrimaryValueBox.Enabled = false;
            this.PrimaryValueBox.Location = new System.Drawing.Point(12, 156);
            this.PrimaryValueBox.Multiline = true;
            this.PrimaryValueBox.Name = "PrimaryValueBox";
            this.PrimaryValueBox.Size = new System.Drawing.Size(210, 154);
            this.PrimaryValueBox.TabIndex = 62;
            this.PrimaryValueBox.Child = new System.Windows.Controls.TextBox();
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1508, 680);
            this.Controls.Add(this.LoadTransforms);
            this.Controls.Add(this.LoadBackup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SecondaryValueBox);
            this.Controls.Add(this.PrimaryValueBox);
            this.Controls.Add(this.adflyUserId);
            this.Controls.Add(this.adflyApiKey);
            this.Controls.Add(this.AdflyResolve);
            this.Controls.Add(this.Domains);
            this.Controls.Add(this.ResolveDomains);
            this.Controls.Add(this.ProgressText);
            this.Controls.Add(this.UpdateProgress);
            this.Controls.Add(this.nextIndex);
            this.Controls.Add(this.prevIndex);
            this.Controls.Add(this.HistoryView);
            this.Controls.Add(this.IgnoreCaseSearch);
            this.Controls.Add(this.VideoSearchText);
            this.Controls.Add(this.SearchVideosButton);
            this.Controls.Add(this.TransformationClear);
            this.Controls.Add(this.SelectSecrets);
            this.Controls.Add(this.TestUpdate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TransformationView);
            this.Controls.Add(this.VideoInformation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IgnoreCaseCheck);
            this.Controls.Add(this.DescriptionUpdateOptions);
            this.Controls.Add(this.UpdateDescriptions);
            this.Controls.Add(this.MethodAddButton);
            this.Controls.Add(this.GetVideos);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private SpellBox SecondaryValueBox;
        private SpellBox PrimaryValueBox;
        private System.Windows.Forms.TextBox adflyUserId;
        private System.Windows.Forms.TextBox adflyApiKey;
        private System.Windows.Forms.Button AdflyResolve;
        private System.Windows.Forms.TextBox Domains;
        private System.Windows.Forms.Button ResolveDomains;
        private System.Windows.Forms.Label ProgressText;
        private System.Windows.Forms.ProgressBar UpdateProgress;
        private System.Windows.Forms.Button nextIndex;
        private System.Windows.Forms.Button prevIndex;
        private System.Windows.Forms.TextBox HistoryView;
        private System.Windows.Forms.CheckBox IgnoreCaseSearch;
        private System.Windows.Forms.TextBox VideoSearchText;
        private System.Windows.Forms.Button SearchVideosButton;
        private System.Windows.Forms.Button TransformationClear;
        private System.Windows.Forms.Button SelectSecrets;
        private System.Windows.Forms.Button TestUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TransformationView;
        private System.Windows.Forms.TextBox VideoInformation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox IgnoreCaseCheck;
        private System.Windows.Forms.ComboBox DescriptionUpdateOptions;
        private System.Windows.Forms.Button UpdateDescriptions;
        private System.Windows.Forms.Button MethodAddButton;
        private System.Windows.Forms.Button GetVideos;
        private System.Windows.Forms.Button LoadBackup;
        private System.Windows.Forms.Button LoadTransforms;
    }
}