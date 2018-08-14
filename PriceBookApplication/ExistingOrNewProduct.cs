using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceBookApplication
{
    public partial class ExistingOrNewProduct : Form
    {
        Product newProduct = new Product();
        public ExistingOrNewProduct()
        {
            InitializeComponent();
        }

        public ExistingOrNewProduct(Product product)
        {
            InitializeComponent();
            newProduct = product;
        }

        public void btnNew_Click(object sender, EventArgs e)
        {
            newProduct.newProduct = true;
            this.Close();
        }

        public void btnExisting_Click(object sender, EventArgs e)
        {
            newProduct.newProduct = false;
            this.Close();
        }
    }
}
