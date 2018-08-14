using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceBookApplication
{
    public partial class Main : Form
    {
        static string dataSource = @"(LocalDB)\MSSQLLocalDB";
        static string attachedDBFile = @"C:\Users\Jade Rickert\source\repos\PriceBookApplication\PriceBookApplication\PriceBook.mdf";
        static string connection = "Data Source =" + dataSource + "; AttachDbFilename=" + attachedDBFile + ";Integrated Security = True; Connect Timeout = 30";
        BindingSource bindingSource = new BindingSource();
        decimal invoiceAmount = 0;
        decimal userInvoiceAmount = 0;
        //bool invoiceFunction = false;
        //bool productFunction = false;
        //bool promoFunction = false;
        //bool storeFunction = false;
        //bool categoryFunction = false;
        //bool reportFunction = false;
        DataGridViewRow row;
        //ExistingOrNewProduct existingOrNew = new ExistingOrNewProduct();
        
        public Main()
        {
            InitializeComponent();
            tcFunctionInput.Visible = false;
            tslblMode.Text = "NONE";
            tslblInvoiceTotal.Text = "";
        }

        //FUNCTION EVENTS
        private void pbxInvoice_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = true;
            //productFunction = false;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = false;
            //reportFunction = false;
            tslblMode.Text = "INVOICE MODE";
            pbxInvoice.BorderStyle = BorderStyle.FixedSingle;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.None;
            loadInvoices();

        }

        private void pbxProduct_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = true;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = false;
            //reportFunction = false;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.FixedSingle;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.None;
            loadProducts();
        }

        private void pbxPromo_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = false;
            //promoFunction = true;
            //storeFunction = false;
            //categoryFunction = false;
            //reportFunction = false;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.FixedSingle;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.None;
        }

        private void pbxCategory_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = false;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = true;
            //reportFunction = false;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.FixedSingle;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.None;
        }

        private void pbxStore_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = false;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = true;
            //reportFunction = false;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.FixedSingle;
            pbxReport.BorderStyle = BorderStyle.None;
        }

        private void pbxReport_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = false;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = false;
            //reportFunction = true;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.FixedSingle;
        }

        //SAVE, VIEW AND CANCEL EVENTS
        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        //2. SAVE BUTTON CLICKS
        
        //SAVE INVOICE DETAILS
        private void btnInvoiceSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                try
                {
                    //SQL START
                    string query;
                    query = "INSERT INTO [dbo].[Transactions] ([Date],[StoreID]," +
                    "[InvoiceNumber],[InvoiceTotalAmount]) VALUES(" +
                    "@date, @store, @invoiceNumber, @invoiceAmount)";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@date", dtpInvoice.Value.Date);
                    sqlCommand.Parameters.AddWithValue("@store", int.Parse(cmbxStore.SelectedValue.ToString()));
                    sqlCommand.Parameters.AddWithValue("@invoiceAmount", decimal.Parse(tbxInvoiceAmount.Text));
                    sqlCommand.Parameters.AddWithValue("@invoiceNumber", dtpInvoice.Value.ToString("yyyyMMdd") + cmbxStore.SelectedValue.ToString() + tbxInvoiceAmount.Text);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    //SQL END
                    MessageBox.Show("Invoice Details Saved!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tcFunctionInput.Visible = false;
                    loadProducts();
                    tslblInvoiceTotal.Text = string.Format("Invoice Total: ${0},", tbxInvoiceAmount.Text);
                    invoiceAmount = decimal.Parse(tbxInvoiceAmount.Text);
                    tslblInvoiceNumber.Text = dtpInvoice.Value.ToString("yyyyMMdd") + cmbxStore.SelectedValue.ToString() + tbxInvoiceAmount.Text;
                    tslblMode.Text = "INVOICE PRODUCT ADD MODE";
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //ADD PRODUCT TO INVOICE
        private void btnInvoiceProductSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                try
                {
                    //SQL START
                    string query;
                    if (tbxWeight.Enabled == false)
                    {
                        query = "INSERT INTO [dbo].[InvoiceProducts] ([VariantID],[Quantity]," +
                                "[TotalPrice],[InvoiceNumber]) VALUES(" +
                                "@variant, @quantity, @totalPrice, @invoiceNumber)";
                    }
                    else
                    {
                        query = "INSERT INTO [dbo].[InvoiceProducts] ([VariantID],[Quantity]," +
                                "[Weight],[TotalPrice],[InvoiceNumber]) VALUES(" +
                                "@variant, @quantity, @weight, @totalPrice, @invoiceNumber)";
                    }
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@variant", int.Parse(row.Cells["VariantID"].Value.ToString()));
                    sqlCommand.Parameters.AddWithValue("@quantity", int.Parse(tbxQuantity.Text));
                    sqlCommand.Parameters.AddWithValue("@totalPrice", decimal.Parse(tbxTotalPrice.Text));
                    sqlCommand.Parameters.AddWithValue("@invoiceNumber", tslblInvoiceNumber.Text);
                    if (tbxWeight.Enabled == true)
                    {
                        sqlCommand.Parameters.AddWithValue("@weight", int.Parse(tbxWeight.Text));
                    }
                    sqlCommand.CommandType = CommandType.Text;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    //SQL END
                    MessageBox.Show("Product Added To Invoice!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    userInvoiceAmount = decimal.Parse(tbxTotalPrice.Text) + userInvoiceAmount;
                    if (differenceCheck())
                    {
                        DialogResult saveInvoiceMessage = MessageBox.Show(string.Format("Invoice Amount: ${0}\nInvoice Products Total: ${1}" +
                            "\nMark Invoice As Saved?", userInvoiceAmount, invoiceAmount),"Invoice",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if(saveInvoiceMessage == DialogResult.Yes)
                        {
                            tcFunctionInput.Visible = false;
                            saveInvoice();
                            loadInvoices();
                        } else if (saveInvoiceMessage == DialogResult.No)
                        {
                            //DO NOTHING
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //ADD NEW VARIANT TO PRODUCT
        private void btnVariantSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                //SQL START
                sqlConnection.Open();
                string query = "insert into [dbo].[Variants] " +
                    "([Description], [Barcode], [BrandName], [PackSize], [ProductCode]) " +
                    "VALUES (@Decription, @Barcode, @BrandName, @PackSize, @ProductCode);";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Decription", tbxDescription02.Text);
                if (tbxBarCode.Text == "")
                {
                    tbxBarCode.Text = " ";
                }
                sqlCommand.Parameters.AddWithValue("@Barcode", tbxBarCode.Text);
                sqlCommand.Parameters.AddWithValue("@BrandName", tbxBrandName.Text);
                sqlCommand.Parameters.AddWithValue("@PackSize", tbxPackSize.Text);
                sqlCommand.Parameters.AddWithValue("@ProductCode", int.Parse(lblProductCode.Text));
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                //SQL END
            }
            MessageBox.Show("Variant Saved", "Variant", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (tslblInvoiceNumber.Text != "")
            {
                tslblMode.Text = "INVOICE PRODUCT ADD MODE";
                tcFunctionInput.Visible = false;
                loadProducts();
            }
        }
        //ADD NEW PRODUCT
        private void btnProductSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                //SQL START
                sqlConnection.Open();
                string query = "insert into [dbo].[Products] " +
                    "([Description], [CategoryID], [MeasurementRate], [UoM], [Weighted]) " +
                    "VALUES (@Decription, @CategoryID, @MeasurementRate, @UoM, @Weighted) " +
                    "SET @ProductCode = SCOPE_IDENTITY(); ";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Decription", tbxDescription.Text);
                sqlCommand.Parameters.AddWithValue("@CategoryID", int.Parse(cmbxCategory.SelectedValue.ToString()));
                sqlCommand.Parameters.AddWithValue("@MeasurementRate", int.Parse(tbxRoM.Text));
                sqlCommand.Parameters.AddWithValue("@UoM", tbxUoM.Text);
                if (cbxWeighted.Checked == false)
                {
                    sqlCommand.Parameters.AddWithValue("@Weighted", 0);
                }
                else if (cbxWeighted.Checked == true)
                {
                    sqlCommand.Parameters.AddWithValue("@Weighted", 1);
                }
                sqlCommand.Parameters.Add("@ProductCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();
                lblProductCode.Text = sqlCommand.Parameters["@ProductCode"].Value.ToString();
                sqlConnection.Close();
                //SQL END
            }
            MessageBox.Show("Product Saved", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tcFunctionInput.SelectedIndex = 2;
            tslblMode.Text = "VARIANT MODE";
        }
        
        //CRUD, SEARCH AND EXPORT EVENTS
        private void pbxAdd_MouseClick(object sender, MouseEventArgs e)
        {
            //ADD NEW INVOICE
            if (tslblMode.Text == "INVOICE MODE")
            {
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 0;
                tslblMode.Text = "INVOICE ADD MODE";
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    try
                    {
                        string query = "SELECT [dbo].[Stores].[StoreID] AS [Store ID], " +
                            "CONCAT([dbo].[Stores].[StoreName], ', ', [dbo].[Stores].[StoreLocation]) AS [Store Name] " +
                            "FROM [dbo].[Stores]";
                        sqlConnection.Open();
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        SqlDataReader sqlDataReader;
                        sqlDataReader = sqlCommand.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("Store Name", typeof(string));
                        dataTable.Columns.Add("Store ID", typeof(string));
                        dataTable.Load(sqlDataReader);
                        cmbxStore.ValueMember = "Store ID";
                        cmbxStore.DisplayMember = "Store Name";
                        cmbxStore.DataSource = dataTable;
                        sqlConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        // write exception info to log or anything else
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            //ADD NEW PRODUCT
            else if (tslblMode.Text == "INVOICE PRODUCT ADD MODE")
            {
                Product product = new Product();

                ExistingOrNewProduct existingOrNewForm = new ExistingOrNewProduct(product);
                existingOrNewForm.ShowDialog();

                if (product.newProduct == true)
                {
                    tcFunctionInput.Visible = true;
                    tcFunctionInput.SelectedIndex = 3;
                    using (SqlConnection sqlConnection = new SqlConnection(connection))
                    {
                        try
                        {
                            string query = "SELECT [dbo].[Categories].[CategoryID] AS [Category ID], " +
                                "CONCAT([dbo].[Categories].[CategoryName], ', ', [dbo].[Categories].[ParentCategory]) AS [Category] " +
                                "FROM [dbo].[Categories]";
                            sqlConnection.Open();
                            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                            SqlDataReader sqlDataReader;
                            sqlDataReader = sqlCommand.ExecuteReader();
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Category", typeof(string));
                            dataTable.Columns.Add("Category ID", typeof(string));
                            dataTable.Load(sqlDataReader);
                            cmbxCategory.ValueMember = "Category ID";
                            cmbxCategory.DisplayMember = "Category";
                            cmbxCategory.DataSource = dataTable;
                            sqlConnection.Close();
                        }
                        catch (Exception ex)
                        {
                            // write exception info to log or anything else
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    tslblMode.Text = "PRODUCT MODE";
                }
                else if (product.newProduct == false)
                {
                    MessageBox.Show("Select Product and Add Variants Details", "New or Existing Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tslblMode.Text = "VARIANT MODE";
                    tcFunctionInput.Visible = true;
                    tcFunctionInput.SelectedIndex = 2;
                }
            }
        }

        //DATA GRID VIEW EVENTS
        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //VIEW INVOICE DETAILS
            if (tslblMode.Text == "INVOICE MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //ADD PRODUCT TO INVOICE
            else if (tslblMode.Text == "INVOICE PRODUCT ADD MODE")
            {
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 1;
                tbxInvoiceAmount.Text = "";
                tbxQuantity.Text = "";
                tbxTotalPrice.Text = "";
                tbxWeight.Text = "";
                cbxSale.Checked = false;
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        //tbxWeight.Text = row.Cells["Weighted"].Value.ToString();
                        if (row.Cells["Weighted"].Value.Equals(false))
                        {
                            tbxWeight.Enabled = false;
                        } else if (row.Cells["Weighted"].Value.Equals(true))
                        {
                            tbxWeight.Enabled = true;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //ADD VARIANT TO PRODUCT
            else if (tslblMode.Text == "VARIANT MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        lblProductCode.Text = row.Cells["ProductCode"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tslblMode.Text == "INVOICE MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    DataTable dataTable = new DataTable();
                    string command = "SELECT " +
                    "CONCAT([dbo].[Variants].[BrandName], ' ', " +
                        "[dbo].[Products].[Description], ' ', " +
                        "[dbo].[Variants].[Description], ' ', " +
                        "[dbo].[Variants].[PackSize], ' ', " +
                        "[dbo].[Products].[UoM]) AS[Product Description], " +
                    "[dbo].[InvoiceProducts].[Quantity] AS[Quantity], " +
                    "[dbo].[InvoiceProducts].[Weight] AS[Weight], " +
                    "[dbo].[InvoiceProducts].[TotalPrice] AS[Total Price] " +
                    "FROM [dbo].[InvoiceProducts]" +
                    "INNER JOIN [dbo].[Variants] ON [dbo].[InvoiceProducts].[VariantID] = [dbo].[Variants].[VariantID]" +
                    "INNER JOIN [dbo].[Products] ON [dbo].[Variants].[ProductCode] = [dbo].[Products].[ProductCode]" +
                    "WHERE [dbo].[InvoiceProducts].[InvoiceNumber] = @InvoiceNumber ";
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@InvoiceNumber", row.Cells["Invoice Number"].Value.ToString());
                    sqlCommand.CommandType = CommandType.Text;
                    dataTable.Load(sqlCommand.ExecuteReader());
                    bindingSource.DataSource = dataTable;
                    dgvMain.DataSource = bindingSource;
                    sqlConnection.Close();
                }
            }
        }

        //METHODS
        //INVOICE
        private void loadProducts()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                DataTable dataTable = new DataTable();
                string command = "select " +
                    "[dbo].[Products].[Description] as [Product Description], " +
                    "[dbo].[Variants].[BrandName] as [Brand Name], " +
                    "[dbo].[Variants].[Description] as [Variant Description], " +
                    "[dbo].[Variants].[PackSize] as [Pack Size], " +
                    "[dbo].[Products].[UoM], " +
                    "[dbo].[Products].[Weighted], " +
                    "[dbo].[Categories].[CategoryName] as [Subcategory], " +
                    "[dbo].[Variants].[VariantID] as [VariantID], " +
                    "[dbo].[Variants].[ProductCode] as [ProductCode] " +
                    "from [dbo].[Variants]" +
                    "inner join [dbo].[Products] on [dbo].[Variants].[ProductCode] = [dbo].[Products].[ProductCode]" +
                    "inner join [dbo].[Categories] on [dbo].[Products].[CategoryID] = [dbo].[Categories].[CategoryID];";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                dataTable.Load(sqlCommand.ExecuteReader());
                bindingSource.DataSource = dataTable;
                dgvMain.DataSource = bindingSource;
                sqlConnection.Close();
            }
        }

        private void loadInvoices()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                DataTable dataTable = new DataTable();
                string command = "SELECT [dbo].[Transactions].[Date] AS [Date], " +
                "CONCAT([dbo].[Stores].[StoreName], ', ', [Stores].[StoreLocation]) AS [Store Name], " +
                "[dbo].[Transactions].[InvoiceNumber] AS [Invoice Number], " +
                "[dbo].[Transactions].[InvoiceTotalAmount] AS[Total Invoice Amount], " +
                "[dbo].[Transactions].[Saved] " +
                        "AS[Saved] " +
                "FROM[dbo].[Transactions] " +
                        "INNER JOIN[dbo].[Stores] ON[dbo].[Transactions].[StoreID] = [dbo].[Stores].[StoreID] " +
                        "ORDER BY[Date] ASC";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                dataTable.Load(sqlCommand.ExecuteReader());
                bindingSource.DataSource = dataTable;
                dgvMain.DataSource = bindingSource;
                sqlConnection.Close();
            }
        }

        private bool differenceCheck()
        {
            double variance = Convert.ToDouble(invoiceAmount) - Convert.ToDouble(userInvoiceAmount);
            if (variance >= -0.11 && variance <= 0.11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void saveInvoice()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                try
                {
                    string query;
                    query = "UPDATE [dbo].[Transactions]" +
                            "SET [dbo].[Transactions].[Saved] = 1" +
                            "WHERE [dbo].[Transactions].[InvoiceNumber] = @invoiceNumber;";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@invoiceNumber", tslblInvoiceNumber.Text);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Show("Invoice Saved", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadInvoices();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
