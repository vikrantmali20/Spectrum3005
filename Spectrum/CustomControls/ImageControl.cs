using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace MOD
{
    public partial class ImageControl : UserControl
    {
        private decimal  _NumberOfButtons=4;
        private decimal  NumberOfButtons
        {
            get
            {
                return _NumberOfButtons;
            }
            set
            {
                _NumberOfButtons = value;
            }
        }
        private int _ButtonTotalColumn=9;
        private int ButtonTotalColumn
        {
            get
            {
                return _ButtonTotalColumn;
            }
            set
            {
                _ButtonTotalColumn = value;
            }
        }

        private System.Drawing.Size ButtonSize
        {
            get;
            set;
        }
        private DataTable _ImageInfo;
        private DataTable ImageInfo
        {
            get
            {
                return _ImageInfo;
            }
            set
            {
                _ImageInfo = value;
            }
        }
        public ImageControl()
        {
           
            InitializeComponent();
            CreateDataTable();
            this.SuspendLayout();
            ButtonSize = new Size(80, 80);
            //this.Width = ButtonSize.Width * ButtonTotalColumn;
            //this.Height = ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2));
            //this.Size = new System.Drawing.Size((ButtonSize.Width) * ButtonTotalColumn, ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2)));
            int validateRow = _ImageInfo.Rows.Count;
            NumberOfButtons = validateRow/100;
            this.tableLayoutPanel1.Size = new System.Drawing.Size((ButtonSize.Width+90) * ButtonTotalColumn, ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2))); ;
           
            CreateButtonsInTabPanel();
            
        }

        public void CreateButtons()
        {
            ImageButton  bt;
            Label lb;
            TableLayoutPanel pn;
            //tableLayoutPanel1.RowCount =  Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2));
            tableLayoutPanel1.ColumnCount = 5;
            int iAddedTotalNoButtons = 0;
            int validateRow = _ImageInfo.Rows.Count;
            NumberOfButtons = 13;
            tableLayoutPanel1.RowCount = Convert.ToInt32(NumberOfButtons / ButtonTotalColumn);

            for (int i = 0; i < NumberOfButtons; i++)
            {
                for (int j = 0; j < ButtonTotalColumn; j++)
                {
                    if (iAddedTotalNoButtons < NumberOfButtons)
                    {
                        this.SuspendLayout();
                        bt = new ImageButton();
                       
                        //bt.BackgroundImage = MOD.Properties.Resources.ExampleButtonA;
                        bt.Size = ButtonSize;
                        Image articleImg = Image.FromFile(string.Format(@"{0}", _ImageInfo.Rows[i]["ImagePath"].ToString()));
                        bt.BackgroundImage = articleImg;
                        bt.HoverImage = articleImg;
                        bt.Text = _ImageInfo.Rows[i]["ArticleCode"].ToString();
                        lb = new Label();
                        lb.Name = "Hi";
                        lb.Text = _ImageInfo.Rows[i]["ArticleCode"].ToString();
                        pn = new TableLayoutPanel();
                        pn.BackColor = System.Drawing.SystemColors.ControlLight;
                        pn.Size = new System.Drawing.Size(ButtonSize.Width + 20, ButtonSize.Height + 20);
                        pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                        pn.RowCount = 2;
                        pn.ColumnCount = 1;
                        pn.Controls.Add(bt, 0, 0);
                        pn.Controls.Add(lb, 0, 1);
                        pn.Margin = new System.Windows.Forms.Padding(0);
                        pn.AutoSize = false;
                        tableLayoutPanel1.Controls.Add(pn, j, i);
                        iAddedTotalNoButtons = iAddedTotalNoButtons + 1;
                       
                    }
                    this.Controls.Add(tableLayoutPanel1);
                    //else  if (iAddedTotalNoButtons < NumberOfButtons && j>=1)
                    //{
                    //    this.SuspendLayout();
                    //    bt = new Button();
                    //    bt.Size = ButtonSize;
                    //    Image articleImg = Image.FromFile(string.Format(@"{0}", _ImageInfo.Rows[i]["ImagePath"].ToString()));
                    //    bt.BackgroundImage = articleImg;
                    //    //lb = new Label();
                    //    //lb.Name = "Hi1236";
                    //    //lb.Text = _ImageInfo.Rows[i]["ArticleCode"].ToString();
                    //    pn = new TableLayoutPanel();
                    //    pn.BackColor = System.Drawing.SystemColors.ControlLight;
                    //    pn.Size = new System.Drawing.Size(ButtonSize.Width + 20, ButtonSize.Height + 20);
                    //    pn.RowCount = 2;
                    //    pn.ColumnCount = 1;
                    //    pn.Controls.Add(bt, 0, 0);
                    //    pn.AutoSize = false;  
                    //    //pn.Controls.Add(lb, 0, 1);

                    //    pn.SetBounds(2, 2, 2, 2, BoundsSpecified.Width);
                    //    tableLayoutPanel1.Controls.Add(pn, 3, i);
                    //    iAddedTotalNoButtons = iAddedTotalNoButtons + 1;

                    //}
                }
            }
           
        }
        public void CreateButtonsInTabPanel()
        {
            tabControl1.Font = new System.Drawing.Font(new FontFamily("Verdana"), 35, GraphicsUnit.Point); 
           
            tabControl1.TabPages[0].Text = "1";
            tabControl1.ShowToolTips = true;  
            tabControl1.TabPages[0].ToolTipText = "Todays Meal";
            tabControl1.TabPages[1].Text = "2";
            tabControl1.TabPages[1].ToolTipText = "Jumbo Meal";
            
            Button    bt;
            Label lb;
            TableLayoutPanel pn;
            //tableLayoutPanel1.RowCount =  Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2));
            tableLayoutPanel1.ColumnCount = ButtonTotalColumn ;
            int iAddedTotalNoButtons = 0;
            int validateRow = _ImageInfo.Rows.Count;
            NumberOfButtons = 27;
            tableLayoutPanel1.RowCount = Convert.ToInt32(NumberOfButtons / ButtonTotalColumn);
            
          
          
            for (int i = 0; i < NumberOfButtons; i++)
            {
                for (int j = 0; j < ButtonTotalColumn; j++)
                {
                    if (iAddedTotalNoButtons < NumberOfButtons)
                    {
                        this.SuspendLayout();
                        bt = new Button();

                        //bt.BackgroundImage = MOD.Properties.Resources.ExampleButtonA;
                        bt.Size = ButtonSize;
                        Image articleImg = Image.FromFile(string.Format(@"{0}", _ImageInfo.Rows[i]["ImagePath"].ToString()));

                        bt.BackgroundImage = (Image)resizeImage(new Bitmap(articleImg, 80, 80), 80, 80);
                        //bt.HoverImage = (Image)resizeImage(new Bitmap(articleImg, 80, 80), 80, 80);
                        //bt.DownImage = (Image)resizeImage(new Bitmap(articleImg, 80, 80), 80, 80);
                        //bt.NormalImage = (Image)resizeImage(new Bitmap(articleImg, 80, 80), 80, 80);
                        //bt.HoverImage = articleImg;
                        
                        lb = new Label ();
                        lb.Name = "Hi";
                        //lb.MaximumSize = new Size (28,80);
                        lb.Anchor =AnchorStyles.Top ;
                        lb.Text = _ImageInfo.Rows[i]["ArticleCode"].ToString();
                        //bt.Text1 = _ImageInfo.Rows[i]["ArticleCode"].ToString();
                        //lb.Text = "Click me ";
                        lb.ForeColor = Color.DarkBlue;
                        lb.Font = new System.Drawing.Font("Vardana", 10, FontStyle.Bold);
                        pn = new TableLayoutPanel();
                        pn.BackColor = Color.SkyBlue;
                        pn.Size = new System.Drawing.Size(ButtonSize.Width + 40, ButtonSize.Height + 40);
                        pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                        bt.Click += new EventHandler(bt_Click);
                        pn.RowCount = 2;
                        pn.ColumnCount = 1;
                        pn.Controls.Add(bt, 0, 0);
                        pn.Controls.Add(lb, 0,1);
                        pn.Margin = new System.Windows.Forms.Padding(0);
                        pn.AutoSize = false;
                        tableLayoutPanel1.Controls.Add(pn, j, i);
                        //tableLayoutPanel2.Controls.Add(bt, 0, 0);
                        //tableLayoutPanel2.Controls.Add(lb, 0, 1);
                        iAddedTotalNoButtons = iAddedTotalNoButtons + 1;

                    }
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ButtonSize.Width+5));
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, ButtonSize.Height+25));
                  




                    //else  if (iAddedTotalNoButtons < NumberOfButtons && j>=1)
                    //{
                    //    this.SuspendLayout();
                    //    bt = new Button();
                    //    bt.Size = ButtonSize;
                    //    Image articleImg = Image.FromFile(string.Format(@"{0}", _ImageInfo.Rows[i]["ImagePath"].ToString()));
                    //    bt.BackgroundImage = articleImg;
                    //    //lb = new Label();
                    //    //lb.Name = "Hi1236";
                    //    //lb.Text = _ImageInfo.Rows[i]["ArticleCode"].ToString();
                    //    pn = new TableLayoutPanel();
                    //    pn.BackColor = System.Drawing.SystemColors.ControlLight;
                    //    pn.Size = new System.Drawing.Size(ButtonSize.Width + 20, ButtonSize.Height + 20);
                    //    pn.RowCount = 2;
                    //    pn.ColumnCount = 1;
                    //    pn.Controls.Add(bt, 0, 0);
                    //    pn.AutoSize = false;  
                    //    //pn.Controls.Add(lb, 0, 1);

                    //    pn.SetBounds(2, 2, 2, 2, BoundsSpecified.Width);
                    //    tableLayoutPanel1.Controls.Add(pn, 3, i);
                    //    iAddedTotalNoButtons = iAddedTotalNoButtons + 1;

                    //}
                }
            }
            
        }

        void bt_Click(object sender, EventArgs e)
        {
           
        }

        public static Image GetResizedImage(Image img, Rectangle rect) 
        { 
            Bitmap b = new Bitmap(rect.Width, rect.Height); 
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, rect.Width, rect.Height);
            g.Dispose(); 
            try { 
                return (Image)b.Clone();
            } 
            finally 
            { 
                b.Dispose(); b = null; g = null;
            } 
        }

        public  Bitmap resizeImage(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);

            return result;
        }

        public void CreateDataTable()
        {
            _ImageInfo = new DataTable();
            _ImageInfo.Columns.Add(new DataColumn("ArticleCode"));
            _ImageInfo.Columns.Add(new DataColumn("ImagePath"));
            _ImageInfo.Columns.Add(new DataColumn("HotKeys"));
             foreach (var item in Directory.GetFiles(@"D:\Images"))
            {
                //FileInfo kl= 
                FileInfo kl =  new FileInfo(item);
                DataRow dr = _ImageInfo.NewRow();
                dr["ArticleCode"] = kl.Name.Replace(kl.Extension,"");
                dr["ImagePath"] = kl.FullName ;
                _ImageInfo.Rows.Add(dr);
            } 

        }
       
    }

    public class GrowLabel : Label
    {
        private bool mGrowing;
        public GrowLabel()
        {
            this.AutoSize = false;
        }
        private void resizeLabel()
        {
            if (mGrowing) return;
            try
            {
                mGrowing = true;
                Size sz = new Size(this.Width, Int32.MaxValue);
                sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                this.Height = sz.Height;
            }
            finally
            {
                mGrowing = false;
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            resizeLabel();
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            resizeLabel();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            resizeLabel();
        }
    }

}
