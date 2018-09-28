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
        //====================================================================//
        //FORM VARIABLES
        //====================================================================//
        static string dataSource = @"(LocalDB)\MSSQLLocalDB";
        static string attachedDBFile = @"C:\Users\CodeBeast\Documents\Visual Studio 2015\Projects\PriceBookApplication\PriceBookApplication\newPriceBook.mdf";
        static string connection = "Data Source =" + dataSource + "; AttachDbFilename=" + attachedDBFile + ";Integrated Security = True; Connect Timeout = 30";
        BindingSource bindingSource = new BindingSource();
        DataGridViewRow row;
        decimal invoiceAmount = 0;
        decimal userInvoiceAmount = 0;
        int invoiceSaved = 0;

        //====================================================================//
        //MAIN FUNCTION
        //====================================================================//
        public Main()
        {
            InitializeComponent();
            tcFunctionInput.Visible = false;
            tslblMode.Text = "NONE";
            tslblInvoiceTotal.Text = "";
            lblStoreID.Visible = false;
        }

        //====================================================================//
        //FUNCTION EVENTS
        //====================================================================//
        //MOUSE CLICK EVENTS
        //====================================================================//

        private void pbxInvoice_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = true;
            //productFunction = false;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = false;
            //reportFunction = false;
            tslblMode.Text = "INVOICE MODE";
            tcFunctionInput.Visible = false;
            bindingSource.RemoveFilter();
            tslblInvoiceNumber.Text = "";
            tslblInvoiceTotal.Text = "";
            tcFunctionInput.Visible = false;
            pbxInvoice.BorderStyle = BorderStyle.FixedSingle;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.None;
            loadInvoices();
            panel1.Visible = true;
            invoiceSaved = 0;
            

        }

        private void pbxProduct_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = true;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = false;
            //reportFunction = false;
            tslblMode.Text = "PRODUCT MODE";
            bindingSource.RemoveFilter();
            tcFunctionInput.Visible = false;
            tslblInvoiceNumber.Text = "";
            tslblInvoiceTotal.Text = "";
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.FixedSingle;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.None;
            loadProducts();
            panel1.Visible = true;
        }

        private void pbxPromo_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = false;
            //promoFunction = true;
            //storeFunction = false;
            //categoryFunction = false;
            //reportFunction = false;
            bindingSource.RemoveFilter();
            tcFunctionInput.Visible = false;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.FixedSingle;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.None;
            panel1.Visible = true;
        }

        private void pbxCategory_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = false;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = true;
            //reportFunction = false;
            bindingSource.RemoveFilter();
            tslblMode.Text = "CATEGORY MODE";
            tcFunctionInput.Visible = false;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.FixedSingle;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.None;
            panel1.Visible = true;
            loadCategories();
        }

        private void pbxStore_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = false;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = true;
            //reportFunction = false;
            bindingSource.RemoveFilter();
            tslblMode.Text = "STORE MODE";
            tcFunctionInput.Visible = false;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.FixedSingle;
            pbxReport.BorderStyle = BorderStyle.None;
            panel1.Visible = true;
            loadStores();
        }

        private void pbxReport_MouseClick(object sender, MouseEventArgs e)
        {
            //invoiceFunction = false;
            //productFunction = false;
            //promoFunction = false;
            //storeFunction = false;
            //categoryFunction = false;
            //reportFunction = true;
            bindingSource.RemoveFilter();
            tcFunctionInput.Visible = false;
            pbxInvoice.BorderStyle = BorderStyle.None;
            pbxProduct.BorderStyle = BorderStyle.None;
            pbxPromo.BorderStyle = BorderStyle.None;
            pbxCategory.BorderStyle = BorderStyle.None;
            pbxStore.BorderStyle = BorderStyle.None;
            pbxReport.BorderStyle = BorderStyle.FixedSingle;
        }

        //====================================================================//
        //ADD, EDIT, DELETE, VIEW, SEARCH, EXPORT AND IMPORT EVENTS
        //====================================================================//
        //MOUSE CLICK EVENTS
        //====================================================================//

        //1.Add
        //====================================================================//
        private void pbxAdd_MouseClick(object sender, MouseEventArgs e)
        {
            //ADD NEW INVOICE
            if (tslblMode.Text == "INVOICE MODE")
            {
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 0;
                lblInvoiceDate.Text = "Invoice Date";
                dtpToDate.Visible = false;
                lblToDate.Visible = false;
                tbxInvoiceAmount.Text = "";
                tbxInvoiceAmount.Enabled = true;
                btnInvoiceSave.Text = "Save";
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
            else if (tslblMode.Text == "INVOICE PRODUCT ADD MODE" || tslblMode.Text == "PRODUCT MODE")
            {
                Product product = new Product();

                ExistingOrNewProduct existingOrNewForm = new ExistingOrNewProduct(product);
                existingOrNewForm.ShowDialog();

                if (product.newProduct == true)
                {
                    tcFunctionInput.Visible = true;
                    tcFunctionInput.SelectedIndex = 3;
                    tbxDescription.Text = "";
                    tbxRoM.Text = "";
                    tbxUoM.Text = "";
                    cbxProductDeleted.Visible = false;
                    btnProductSave.Text = "Add";
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
                    loadProductsOnly();
                    MessageBox.Show("Select Product and Add Variants Details", "New or Existing Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tslblMode.Text = "VARIANT MODE";
                    tcFunctionInput.Visible = true;
                    tcFunctionInput.SelectedIndex = 2;
                    tbxDescription02.Text = "";
                    tbxBarCode.Text = "";
                    tbxBrandName.Text = "";
                    tbxPackSize.Text = "";
                    cbxVariantDeleted.Visible = false;
                }
            }
            //ADD INVOICE TO EXISTING INVOICE
            else if (tslblMode.Text == "INVOICE VIEW MODE")
            {
                loadProducts();
                invoiceAmount = decimal.Parse(tslblInvoiceTotal.Text);
                tslblMode.Text = "INVOICE PRODUCT ADD MODE";
            }
            //ADD STORE
            else if (tslblMode.Text == "STORE MODE")
            {
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 4;
                tslblMode.Text = "STORE ADD MODE";
            }
            //ADD STORE
            else if (tslblMode.Text == "CATEGORY MODE")
            {
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 5;
                tslblMode.Text = "CATEGORY ADD MODE";
            }
        }

        //2.View
        //====================================================================//
        private void pbxView_Click(object sender, EventArgs e)
        {
            if (tslblMode.Text == "INVOICE MODE")
            {
                try
                {
                    tslblInvoiceNumber.Text = row.Cells["Invoice Number"].Value.ToString();
                    tslblInvoiceTotal.Text = row.Cells["Total Invoice Amount"].Value.ToString();
                    invoiceSaved = Convert.ToInt32(row.Cells["Saved"].Value);
                    using (SqlConnection sqlConnection = new SqlConnection(connection))
                    {
                        sqlConnection.Open();
                        DataTable dataTable = new DataTable();
                        string command = "SELECT " +
                        "[dbo].[Variants].[BrandName] AS [Brand Name]," +
                        "[dbo].[Products].[Description] AS [Product Desc]," +
                        "[dbo].[Variants].[Description] AS [Variant Desc]," +
                        "[dbo].[Variants].[PackSize] AS [Pack Size]," +
                        "[dbo].[Products].[UoM] AS [UOM]," +
                        "[dbo].[InvoiceProducts].[Quantity] AS [Quantity], " +
                        "[dbo].[InvoiceProducts].[Weight] AS [Weight], " +
                        "[dbo].[InvoiceProducts].[TotalPrice] AS [Total Price], " +
                        "[dbo].[InvoiceProducts].[Sale] AS [Sale]," +
                        "[dbo].[InvoiceProducts].[VariantID] AS [Stock Code]" +
                        "FROM [dbo].[InvoiceProducts]" +
                        "INNER JOIN [dbo].[Variants] ON [dbo].[InvoiceProducts].[VariantID] = [dbo].[Variants].[VariantID]" +
                        "INNER JOIN [dbo].[Products] ON [dbo].[Variants].[ProductCode] = [dbo].[Products].[ProductCode]" +
                        "WHERE [dbo].[InvoiceProducts].[InvoiceNumber] = @InvoiceNumber ";
                        SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                        try
                        {
                            sqlCommand.Parameters.AddWithValue("@InvoiceNumber", row.Cells["Invoice Number"].Value.ToString());
                            sqlCommand.CommandType = CommandType.Text;
                            dataTable.Load(sqlCommand.ExecuteReader());
                            bindingSource.DataSource = dataTable;
                            dgvMain.DataSource = bindingSource;
                            tslblMode.Text = "INVOICE VIEW MODE";
                            userInvoiceAmount = getSum();
                            invoiceAmount = Convert.ToDecimal(tslblInvoiceTotal.Text);
                            if (differenceCheck() && invoiceSaved == 0)
                            {
                                DialogResult saveInvoiceMessage = MessageBox.Show(string.Format("Invoice Amount: ${0}\nInvoice Products Total: ${1}" +
                                    "\nMark Invoice As Saved?", userInvoiceAmount, invoiceAmount), "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (saveInvoiceMessage == DialogResult.Yes)
                                {
                                    tcFunctionInput.Visible = false;
                                    saveInvoice();
                                    loadInvoices();
                                    tslblMode.Text = "INVOICE MODE";
                                    invoiceAmount = 0;
                                    userInvoiceAmount = 0;
                                    tslblInvoiceTotal.Text = "";
                                    tslblInvoiceNumber.Text = "";
                                }
                                else if (saveInvoiceMessage == DialogResult.No)
                                {
                                    //DO NOTHING
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        sqlConnection.Close();
                    }
                } catch
                {
                    MessageBox.Show("Select An Invoice First!", "Invoice Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //3.Edit
        //====================================================================//
        private void pbxEdit_MouseClick(object sender, MouseEventArgs e)
        {
            //EDIT INVOICE
            if (tslblMode.Text == "INVOICE MODE")
            {
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 0;
                lblInvoiceDate.Text = "Invoice Date";
                dtpToDate.Visible = false;
                lblToDate.Visible = false;
                tbxInvoiceAmount.Enabled = true;
                btnInvoiceSave.Text = "Edit";
                //Fill the combobox with Store Data
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
                tbxInvoiceAmount.Text = row.Cells["Total Invoice Amount"].Value.ToString();
                dtpInvoice.Value = Convert.ToDateTime(row.Cells["Date"].Value);
                tslblInvoiceNumber.Text = row.Cells["Invoice Number"].Value.ToString();
                tslblMode.Text = "INVOICE EDIT MODE";
            }
            //EDIT PRODUCT
            else if (tslblMode.Text == "PRODUCT MODE" || tslblMode.Text == "PRODUCT SEARCH MODE" || tslblMode.Text == "VARIANT SEARCH MODE")
            {
                Product product = new Product();
                if (tslblMode.Text == "PRODUCT MODE")
                {
                    bool productOrVariant = true;
                    ExistingOrNewProduct existingOrNewForm = new ExistingOrNewProduct(product, productOrVariant);
                    existingOrNewForm.ShowDialog();
                }
                else if (tslblMode.Text == "PRODUCT SEARCH MODE")
                {
                    product.newProduct = true;
                }
                else if (tslblMode.Text == "VARIANT SEARCH MODE")
                {
                    product.newProduct = false;
                }

                if (product.newProduct == true)
                {
                    tcFunctionInput.Visible = true;
                    tcFunctionInput.SelectedIndex = 3;
                    cbxProductDeleted.Visible = false;
                    btnProductSave.Text = "Edit";
                    tslblMode.Text = "PRODUCT EDIT MODE";
                    MessageBox.Show("Select Product and Edit Product Details", "Editing Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
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
                }
                //EDIT VARIANT DETAILS
                else if (product.newProduct == false)
                {
                    MessageBox.Show("Select Product and Edit Variants Details", "Editing Variant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tslblMode.Text = "VARIANT EDIT MODE";
                    btnVariantSave.Text = "Edit";
                    tcFunctionInput.Visible = true;
                    tcFunctionInput.SelectedIndex = 2;
                    cbxVariantDeleted.Visible = false;
                }
            }
            //EDIT STORE DETAILS
            else if (tslblMode.Text == "STORE MODE")
            {
                MessageBox.Show("Select Store and Edit Store Details", "Editing Store", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 4;
                tslblMode.Text = "STORE EDIT MODE";
            }
            //EDIT CATEGORY DETAILS
            else if (tslblMode.Text == "CATEGORY MODE")
            {
                MessageBox.Show("Select Category and Edit Category Details", "Editing Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 5;
                tslblMode.Text = "CATEGORY EDIT MODE";
            }
        }

        //4.Delete
        //====================================================================//
        private void pbxDelete_MouseClick(object sender, MouseEventArgs e)
        {
             
            if (tslblMode.Text == "INVOICE MODE")
            {
                try
                {
                    if (Convert.ToInt32(row.Cells["Saved"].Value) != 1)
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connection))
                        {
                            sqlConnection.Open();
                            string command =    "DELETE FROM [dbo].[Transactions] " +
                                                "WHERE [dbo].[Transactions].[InvoiceNumber] = @InvoiceNumber; ";
                            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                            try
                            {
                                sqlCommand.Parameters.AddWithValue("@InvoiceNumber", row.Cells["Invoice Number"].Value.ToString());
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.ExecuteNonQuery();
                                sqlConnection.Close();
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                MessageBox.Show("Cannot delete invoice with products attached");
                                MessageBox.Show(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        MessageBox.Show("Invoice Deleted!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tcFunctionInput.Visible = false;
                        loadInvoices();
                        tslblMode.Text = "INVOICE MODE";
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete a Saved Invoice");
                    }
                }
                catch (System.NullReferenceException ex)
                {
                    MessageBox.Show("Select a row first");
                    MessageBox.Show(ex.Message);
                }
            }
            //DELETE PRODUCT FROM INVOICE
            else if(tslblMode.Text == "INVOICE VIEW MODE")
            {
                try
                {
                    if (invoiceSaved == 0)
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connection))
                        {
                            sqlConnection.Open();
                            string command = "DELETE FROM [dbo].[InvoiceProducts] " +
                            "WHERE [dbo].[InvoiceProducts].[InvoiceNumber] = @InvoiceNumber " +
                            "AND [dbo].[InvoiceProducts].[VariantID] = @Variant";
                            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                            try
                            {
                                sqlCommand.Parameters.AddWithValue("@InvoiceNumber", tslblInvoiceNumber.Text);
                                sqlCommand.Parameters.AddWithValue("@Variant", Convert.ToInt32(row.Cells["Stock Code"].Value));
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.ExecuteNonQuery();
                                sqlConnection.Close();
                                //SQL END
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                MessageBox.Show("Something went wrong");
                                MessageBox.Show(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                //MessageBox.Show(string.Format("{0}", Convert.ToInt32(row.Cells["Stock Code"].Value)));
                            }
                        }
                        MessageBox.Show("Product Deleted from Invoice!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete products on a saved invoice");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Select a row first");
                    MessageBox.Show(ex.Message);
                }
            }
            //DELETE PRODUCT
            else if (tslblMode.Text == "PRODUCT MODE")
            {
                Product product = new Product();
                bool productOrVariant = true;

                ExistingOrNewProduct existingOrNewForm = new ExistingOrNewProduct(product, productOrVariant);
                existingOrNewForm.ShowDialog();

                if (product.newProduct == true)
                {
                    tcFunctionInput.Visible = false;
                    tslblMode.Text = "PRODUCT DELETE MODE";
                    MessageBox.Show("Select Product To Delete", "Deleting Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (product.newProduct == false)
                {
                    tcFunctionInput.Visible = false;
                    tslblMode.Text = "VARIANT DELETE MODE";
                    MessageBox.Show("Select Variant To Delete", "Deleting Variant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //DELETE STORE
            else if (tslblMode.Text == "STORE MODE")
            {
                tcFunctionInput.Visible = false;
                tslblMode.Text = "STORE DELETE MODE";
                MessageBox.Show("Select Store To Delete", "Deleting Store", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //DELETE CATEGORY
            else if (tslblMode.Text == "CATEGORY MODE")
            {
                tcFunctionInput.Visible = false;
                tslblMode.Text = "CATEGORY DELETE MODE";
                MessageBox.Show("Select Category To Delete", "Deleting Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //5. Search
        //====================================================================//
        private void pbxSearch_Click(object sender, EventArgs e)
        {
            //SEARCH INVOICE
            if (tslblMode.Text == "INVOICE MODE")
            {
                tcFunctionInput.Visible = true;
                tcFunctionInput.SelectedIndex = 0;
                tbxInvoiceAmount.Text = "";
                lblInvoiceDate.Text = "From Date";
                dtpToDate.Visible = true;
                lblToDate.Visible = true;
                tbxInvoiceAmount.Enabled = false;
                btnInvoiceSave.Text = "Search";
                tslblMode.Text = "INVOICE SEARCH MODE";
                dtpInvoice.Value = DateTime.Now.AddDays(-30);
                dtpToDate.Value = DateTime.Now;
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
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["Store Name"] = "<ALL>";
                        dataRow["Store ID"] = "0";
                        dataTable.Rows.Add(dataRow);
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
            //================================================================================================================//
            //SEARCH PRODUCT AND VARIANT
            if (tslblMode.Text == "PRODUCT MODE")
            {
                Product product = new Product();
                bool productOrVariant = true;

                ExistingOrNewProduct existingOrNewForm = new ExistingOrNewProduct(product, productOrVariant);
                existingOrNewForm.ShowDialog();

                //SEARCH PRODUCT
                if (product.newProduct == true)
                {
                    tcFunctionInput.Visible = true;
                    tcFunctionInput.SelectedIndex = 3;
                    tbxDescription.Text = "";
                    tbxRoM.Text = "";
                    tbxUoM.Text = "";
                    cbxProductDeleted.Visible = true;
                    btnProductSave.Text = "Search";
                    tslblMode.Text = "PRODUCT SEARCH MODE";
                    using (SqlConnection sqlConnection = new SqlConnection(connection))
                    {
                        try
                        {
                            string query = "SELECT [dbo].[Categories].[CategoryID] AS [Category ID], " +
                                "[dbo].[Categories].[CategoryName] AS [Category]" +
                                "FROM [dbo].[Categories]";
                            sqlConnection.Open();
                            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                            SqlDataReader sqlDataReader;
                            sqlDataReader = sqlCommand.ExecuteReader();
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Category", typeof(string));
                            dataTable.Columns.Add("Category ID", typeof(string));
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["Category"] = "<ALL>";
                            dataRow["Category ID"] = "0";
                            dataTable.Rows.Add(dataRow);
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
                }
                //SEARCH VARIANT
                else if (product.newProduct == false)
                {
                    tcFunctionInput.Visible = true;
                    tcFunctionInput.SelectedIndex = 2;
                    tbxDescription02.Text = "";
                    tbxBarCode.Text = "";
                    tbxBrandName.Text = "";
                    tbxPackSize.Text = "";
                    cbxVariantDeleted.Visible = true;
                    btnVariantSave.Text = "Search";
                    tslblMode.Text = "VARIANT SEARCH MODE";
                }
            }
        }

        //6. Import
        //====================================================================//


        //7. Export
        //====================================================================//

        
        //====================================================================//
        //TAB PAGE SAVE AND CANCEL BUTTON EVENTS
        //====================================================================//
        //BUTTON CLICK EVENT
        //====================================================================//


        //1. Save, Edit, Search & Cancel
        //====================================================================//
        //A. Invoice Details
        private void btnInvoiceSave_Click(object sender, EventArgs e)
        {
            //I. SAVE INVOICE
            if(tslblMode.Text == "INVOICE ADD MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    try
                    {
                        string query;
                        query = "INSERT INTO [dbo].[Transactions] ([Date],[StoreID]," +
                        "[InvoiceNumber],[InvoiceTotalAmount]) VALUES (" +
                        "@date, @store, @invoiceNumber, @invoiceAmount) ";
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@date", dtpInvoice.Value.Date);
                        sqlCommand.Parameters.AddWithValue("@store", int.Parse(cmbxStore.SelectedValue.ToString()));
                        sqlCommand.Parameters.AddWithValue("@invoiceAmount", decimal.Parse(tbxInvoiceAmount.Text));
                        string invoiceNumber = string.Format("{0}{1}{2}", dtpInvoice.Value.ToString("yyyyMMdd"), cmbxStore.SelectedValue.ToString(), invoiceNumberGenerator());
                        sqlCommand.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
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
                        tslblInvoiceNumber.Text = invoiceNumber;
                        //tslblInvoiceNumber.Text = dtpInvoice.Value.ToString("yyyyMMdd") + cmbxStore.SelectedValue.ToString() + tbxInvoiceAmount.Text;
                        tslblMode.Text = "INVOICE PRODUCT ADD MODE";
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //II. EDIT INVOICE
            else if (tslblMode.Text == "INVOICE EDIT MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    try
                    {
                        string query;
                        query = "UPDATE [dbo].[Transactions] " +
                            "SET [Date] = @date, [StoreID] = @store, [InvoiceTotalAmount] = @invoiceAmount " +
                            "WHERE [InvoiceNumber] = @invoiceNumber";
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@date", dtpInvoice.Value.Date);
                        sqlCommand.Parameters.AddWithValue("@store", int.Parse(cmbxStore.SelectedValue.ToString()));
                        sqlCommand.Parameters.AddWithValue("@invoiceAmount", decimal.Parse(tbxInvoiceAmount.Text));
                        sqlCommand.Parameters.AddWithValue("@invoiceNumber", tslblInvoiceNumber.Text);
                        sqlCommand.CommandType = CommandType.Text;
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        MessageBox.Show("Invoice Details Edited!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tcFunctionInput.Visible = false;
                        loadInvoices();
                        tslblMode.Text = "INVOICE MODE";
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //III. SEARCH INVOICE
            else if (tslblMode.Text == "INVOICE SEARCH MODE")
            {
                try
                {
                    if (cmbxStore.Text == "<ALL>")
                    {
                        bindingSource.Filter = string.Format("[Date] >= '{1:yyyy-MM-dd}' AND [Date] <= '{2:yyyy-MM-dd}'",
                            cmbxStore.Text, dtpInvoice.Value, dtpToDate.Value);
                    }
                    else if (cmbxStore.Text != "<ALL>")
                    {
                        bindingSource.Filter = string.Format("[Store Name] like '%{0}%' AND [Date] >= '{1:yyyy-MM-dd}' AND [Date] <= '{2:yyyy-MM-dd}'",
                            cmbxStore.Text, dtpInvoice.Value, dtpToDate.Value);
                    }
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }
        
        //B. Variant of Product
        private void btnVariantSave_Click(object sender, EventArgs e)
        {
            //I. SAVE VARIANT DETAILS
            if (tslblMode.Text == "VARIANT MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
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
                }
                MessageBox.Show("Variant Saved", "Variant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadProducts();
                tslblMode.Text = "PRODUCT MODE";
                tcFunctionInput.Visible = false;
                if (tslblInvoiceNumber.Text != "")
                {
                    tslblMode.Text = "INVOICE PRODUCT ADD MODE";
                }
            }
            //II. EDIT VARIANT DETAILS
            else if (tslblMode.Text == "VARIANT EDIT MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    string query = "UPDATE [dbo].[Variants] " +
                            "SET [Description] = @Decription, [BrandName] = @BrandName, [PackSize] = @PackSize, [Barcode] = @Barcode " +
                            "WHERE [dbo].[Variants].[VariantID] = @variantID";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@Decription", tbxDescription02.Text);
                    if (tbxBarCode.Text == "")
                    {
                        tbxBarCode.Text = " ";
                    }
                    sqlCommand.Parameters.AddWithValue("@Barcode", tbxBarCode.Text);
                    sqlCommand.Parameters.AddWithValue("@BrandName", tbxBrandName.Text);
                    sqlCommand.Parameters.AddWithValue("@PackSize", tbxPackSize.Text);
                    sqlCommand.Parameters.AddWithValue("@variantID", int.Parse(lblProductCode.Text));
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                MessageBox.Show("Variant Edited", "Variant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadProducts();
                tcFunctionInput.Visible = false;
            }
            //III. SEARCH VARIANT Details
            else if (tslblMode.Text == "VARIANT SEARCH MODE")
            {
                try
                {
                    string search = "";
                    int searchCount = 0;

                    //1. Variant Description
                    if (tbxDescription02.Text != "")
                    {
                        search = "[Variant Description] like '%" + tbxDescription02.Text + "%'";
                        searchCount++;
                    }
                    //2. Barcode
                    if (tbxBarCode.Text != "")
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[Bar Code] like '%" + tbxBarCode.Text + "%'";
                        searchCount++;
                    }
                    //3. Brand Name
                    if (tbxBrandName.Text != "")
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[Brand Name] like '%" + tbxBrandName.Text + "%'";
                        searchCount++;
                    }
                    //4. Pack Size
                    if (tbxPackSize.Text != "")
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[Pack Size] = " + tbxPackSize.Text;
                        searchCount++;
                    }
                    //6. Deleted
                    if (cbxVariantDeleted.Checked == true)
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[Variant Deleted] = 1";
                        searchCount++;
                    }
                    bindingSource.Filter = search;
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }

        }


        //C. Product
        private void btnProductSave_Click(object sender, EventArgs e)
        {
            //I. SAVE PRODUCT
            if(tslblMode.Text == "PRODUCT MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
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
                }
                MessageBox.Show("Product Saved", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tcFunctionInput.SelectedIndex = 2;
                tbxDescription02.Text = "";
                tbxBarCode.Text = "";
                tbxBrandName.Text = "";
                tbxPackSize.Text = "";
                tslblMode.Text = "VARIANT MODE";
            }
            //II. EDIT PRODUCT
            else if (tslblMode.Text == "PRODUCT EDIT MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    try
                    {
                        string query;
                        query = "UPDATE [dbo].[Products] " +
                            "SET [Description] = @description, [UoM] = @uom, [Weighted] = @weighted, [MeasurementRate] = @rom, [CategoryID] = @category " +
                            "WHERE [dbo].[Products].[ProductCode] = @productCode";
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@description", tbxDescription.Text);
                        sqlCommand.Parameters.AddWithValue("@category", int.Parse(cmbxCategory.SelectedValue.ToString()));
                        sqlCommand.Parameters.AddWithValue("@rom", int.Parse(tbxRoM.Text));
                        sqlCommand.Parameters.AddWithValue("@uom", tbxUoM.Text);
                        if (cbxWeighted.Checked == false)
                        {
                            sqlCommand.Parameters.AddWithValue("@weighted", 0);
                        }
                        else if (cbxWeighted.Checked == true)
                        {
                            sqlCommand.Parameters.AddWithValue("@weighted", 1);
                        }
                        sqlCommand.Parameters.AddWithValue("@productCode", lblProductCode2.Text);
                        sqlCommand.CommandType = CommandType.Text;
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        MessageBox.Show("Product Details Edited!", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tcFunctionInput.Visible = false;
                        loadProducts();
                        tslblMode.Text = "PRODUCT MODE";
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //III. SEARCH PRODUCT
            else if (tslblMode.Text == "PRODUCT SEARCH MODE")
            {
                try
                {
                    string search = "";
                    int searchCount = 0;

                    //1. Product Description
                    if (tbxDescription.Text != "")
                    {
                        search = "[Product Description] like '%" + tbxDescription.Text + "%'";
                        searchCount++; 
                    }
                    //2. Category
                    if (cmbxCategory.Text != "<ALL>")
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[Subcategory] like '%" + cmbxCategory.Text + "%'";
                        searchCount++;
                    }
                    //3. Rate of Measurement
                    if (tbxRoM.Text != "")
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[Rate of Measure] = " + tbxRoM.Text;
                        searchCount++;
                    }
                    //4. Unit of Measure
                    if (tbxUoM.Text != "")
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[UoM] = " + tbxUoM.Text;
                        searchCount++;
                    }
                    //5. Weighted
                    if (cbxWeighted.Checked == true)
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[Weighted] = 1";
                        searchCount++;
                    }
                    //6. Deleted
                    if (cbxProductDeleted.Checked == true)
                    {
                        if (searchCount > 0)
                        {
                            search = search + " AND ";
                        }
                        search = search + "[Product Deleted] = 1";
                        searchCount++;
                    }
                    bindingSource.Filter = search;
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            
        }


        //D. Store
        private void btnSaveStore_Click(object sender, EventArgs e)
        {
            //I. SAVE STORE
            if (tslblMode.Text == "STORE ADD MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    try
                    {
                        string query;
                        query = "INSERT INTO [dbo].[Stores] ([StoreName],[StoreLocation]) VALUES (" +
                        "@name, @location) ";
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@name", tbxStoreName.Text);
                        sqlCommand.Parameters.AddWithValue("@location", tbxStoreLocation.Text);
                        sqlCommand.CommandType = CommandType.Text;
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        MessageBox.Show("Store Saved!", "Store", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tcFunctionInput.Visible = false;
                        loadStores();
                        tslblMode.Text = "STORE MODE";
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //II. EDIT STORE
            else if (tslblMode.Text == "STORE EDIT MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    try
                    {
                        string query;
                        query = "UPDATE [dbo].[Stores] " +
                            "SET [StoreName] = @name, [StoreLocation] = @location " +
                            "WHERE [StoreID] = @id";
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@name", tbxStoreName.Text);
                        sqlCommand.Parameters.AddWithValue("@location", tbxStoreLocation.Text);
                        sqlCommand.Parameters.AddWithValue("@id", Int32.Parse(lblStoreID.Text));
                        sqlCommand.CommandType = CommandType.Text;
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        MessageBox.Show("Store Details Edited!", "Store", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tcFunctionInput.Visible = false;
                        loadStores();
                        tslblMode.Text = "STORE MODE";
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        
        //E. Category
        private void btnCategorySave_Click(object sender, EventArgs e)
        {
            //I. SAVE CATEGORY
            if (tslblMode.Text == "CATEGORY ADD MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    try
                    {
                        //SQL START
                        string query;
                        query = "INSERT INTO [dbo].[Categories] ([ParentCategory],[CategoryName]) VALUES (" +
                        "@main, @sub) ";
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@main", tbxMainCategory.Text);
                        sqlCommand.Parameters.AddWithValue("@sub", tbxSubcategory.Text);
                        sqlCommand.CommandType = CommandType.Text;
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        //SQL END
                        MessageBox.Show("Category Saved!", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tcFunctionInput.Visible = false;
                        loadCategories();
                        tslblMode.Text = "CATEGORY MODE";
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            //II. EDIT CATEGORY
            else if (tslblMode.Text == "CATEGORY EDIT MODE")
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    try
                    {
                        //SQL START
                        string query;
                        query = "UPDATE [dbo].[Categories] " +
                            "SET [ParentCategory] = @main, [CategoryName] = @sub " +
                            "WHERE [CategoryID] = @id";
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@main", tbxMainCategory.Text);
                        sqlCommand.Parameters.AddWithValue("@sub", tbxSubcategory.Text);
                        sqlCommand.Parameters.AddWithValue("@id", Int32.Parse(lblCategoryID.Text));
                        sqlCommand.CommandType = CommandType.Text;
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        //SQL END
                        MessageBox.Show("Category Details Edited!", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tcFunctionInput.Visible = false;
                        loadCategories();
                        tslblMode.Text = "CATEGORY MODE";
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        
        
        //2. Add Product To Invoice
        //====================================================================//
        private void btnInvoiceProductSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                try
                {
                    string query;
                    if (tbxWeight.Enabled == false)
                    {
                        query = "INSERT INTO [dbo].[InvoiceProducts] ([VariantID],[Quantity]," +
                                "[TotalPrice],[InvoiceNumber],[Sale]) VALUES(" +
                                "@variant, @quantity, @totalPrice, @invoiceNumber, @sale)";
                    }
                    else
                    {
                        query = "INSERT INTO [dbo].[InvoiceProducts] ([VariantID],[Quantity]," +
                                "[Weight],[TotalPrice],[InvoiceNumber],[Sale]) VALUES(" +
                                "@variant, @quantity, @weight, @totalPrice, @invoiceNumber, @sale)";
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
                    if (cbxSale.Checked == false)
                    {
                        sqlCommand.Parameters.AddWithValue("@sale", 0);
                    }
                    else if (cbxSale.Checked == true)
                    {
                        sqlCommand.Parameters.AddWithValue("@sale", 1);
                    }
                    sqlCommand.CommandType = CommandType.Text;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Show("Product Added To Invoice!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    userInvoiceAmount = decimal.Parse(tbxTotalPrice.Text) + userInvoiceAmount;
                    if (differenceCheck())
                    {
                        DialogResult saveInvoiceMessage = MessageBox.Show(string.Format("Invoice Amount: ${0}\nInvoice Products Total: ${1}" +
                            "\nMark Invoice As Saved?", userInvoiceAmount, invoiceAmount), "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (saveInvoiceMessage == DialogResult.Yes)
                        {
                            tcFunctionInput.Visible = false;
                            saveInvoice();
                            loadInvoices();
                            tslblMode.Text = "INVOICE MODE";
                            invoiceAmount = 0;
                            userInvoiceAmount = 0;
                            tslblInvoiceTotal.Text = "";
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        //====================================================================//
        //DATA GRID VIEW EVENTS
        //====================================================================//
        //MOUSE CLICK EVENT
        //====================================================================//

        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //1. VIEW, ADD INVOICE PRODUCT, VIEW MODE, 
            //====================================================================//
            //A. Invoice Details
            //I. VIEW INVOICE DETAILS
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
            //II. ADD PRODUCT TO INVOICE
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
            //III. SELECT ROW IN VIEW MODE
            else if (tslblMode.Text == "INVOICE VIEW MODE")
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
            //B. Variant of Product
            //1. ADD, EDIT, DELETE
            //====================================================================//
            
            //I. ADD VARIANT
            else if (tslblMode.Text == "VARIANT MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        lblProductCode.Text = row.Cells["ProductCode"].Value.ToString();
                        lblUoMVariant.Text = row.Cells["UoM"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //II. EDIT VARIANT DETAILS
            else if (tslblMode.Text == "VARIANT EDIT MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        tbxDescription02.Text = row.Cells["Variant Description"].Value.ToString();
                        tbxBarCode.Text = row.Cells["Bar Code"].Value.ToString();
                        tbxBrandName.Text = row.Cells["Brand Name"].Value.ToString();
                        tbxPackSize.Text = row.Cells["Pack Size"].Value.ToString();
                        lblUoMVariant.Text = row.Cells["UoM"].Value.ToString();
                        lblProductCode.Text = row.Cells["VariantID"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //III. DELETE VARIANT
            else if (tslblMode.Text == "VARIANT DELETE MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        DialogResult dialog = MessageBox.Show(string.Format("Are you sure you want to delete:\n{0} {1} {2} {3}",
                            row.Cells["VariantID"].Value.ToString(), row.Cells["Brand Name"].Value.ToString(),
                            row.Cells["Variant Description"].Value.ToString(), row.Cells["Pack Size"].Value.ToString()),
                            "Delete Variant", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.Yes)
                        {
                            using (SqlConnection sqlConnection = new SqlConnection(connection))
                            {
                                try
                                {
                                    //SQL START
                                    string query;
                                    query = "UPDATE [dbo].[Variants] " +
                                        "SET [Deleted] = 1 " +
                                        "WHERE [dbo].[Variants].[VariantID] = @variantID";
                                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("@variantID", Convert.ToInt32(row.Cells["VariantID"].Value.ToString()));
                                    sqlCommand.CommandType = CommandType.Text;
                                    sqlConnection.Open();
                                    sqlCommand.ExecuteNonQuery();
                                    sqlConnection.Close();
                                    //SQL END
                                    MessageBox.Show("Variant Deleted!", "Variant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tcFunctionInput.Visible = false;
                                    loadProducts();
                                    tslblMode.Text = "PRODUCT MODE";
                                }
                                catch (System.Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        else if (dialog == DialogResult.Cancel)
                        {
                            tslblMode.Text = "PRODUCT MODE";
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //C. Product
            //1. EDIT, DELETE
            //====================================================================//

            //I. EDIT PRODUCT DETAILS
            else if (tslblMode.Text == "PRODUCT EDIT MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        tbxDescription.Text = row.Cells["Product Description"].Value.ToString();
                        tbxRoM.Text = row.Cells["Rate of Measure"].Value.ToString();
                        tbxUoM.Text = row.Cells["UoM"].Value.ToString();
                        if (row.Cells["Weighted"].Value.Equals(false))
                        {
                            tbxWeight.Enabled = false;
                        }
                        else if (row.Cells["Weighted"].Value.Equals(true))
                        {
                            tbxWeight.Enabled = true;
                        }
                        lblProductCode2.Text = row.Cells["ProductCode"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //II. DELETE PRODUCT
            else if (tslblMode.Text == "PRODUCT DELETE MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        DialogResult dialog = MessageBox.Show(string.Format("Are you sure you want to delete:\n{0} {1} {2}", 
                            row.Cells["ProductCode"].Value.ToString(), row.Cells["Product Description"].Value.ToString(), row.Cells["UoM"].Value.ToString()), 
                            "Delete Product", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.Yes)
                        {
                            using (SqlConnection sqlConnection = new SqlConnection(connection))
                            {
                                try
                                {
                                    string query;
                                    query = "UPDATE [dbo].[Products] " +
                                        "SET [Deleted] = 1 " +
                                        "WHERE [dbo].[Products].[ProductCode] = @productCode";
                                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("@productCode", Convert.ToInt32(row.Cells["ProductCode"].Value.ToString()));
                                    sqlCommand.CommandType = CommandType.Text;
                                    sqlConnection.Open();
                                    sqlCommand.ExecuteNonQuery();
                                    sqlConnection.Close();
                                    MessageBox.Show("Product Deleted!", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tcFunctionInput.Visible = false;
                                    loadProducts();
                                    tslblMode.Text = "PRODUCT MODE";
                                }
                                catch (System.Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        else if (dialog == DialogResult.Cancel)
                        {
                            tslblMode.Text = "PRODUCT MODE";
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //D. Store
            //1. EDIT, DELETE
            //====================================================================//
            //I. EDIT STORE
            else if (tslblMode.Text == "STORE EDIT MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        tbxStoreName.Text = row.Cells["Store Name"].Value.ToString();
                        tbxStoreLocation.Text = row.Cells["Store Location"].Value.ToString();
                        lblStoreID.Text = row.Cells["StoreID"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //II. DELETE STORE
            else if (tslblMode.Text == "STORE DELETE MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        DialogResult dialog = MessageBox.Show(string.Format("Are you sure you want to delete:\n{0} - {1}, {2}",
                            row.Cells["StoreID"].Value.ToString(), row.Cells["Store Name"].Value.ToString(), row.Cells["Store Location"].Value.ToString()),
                            "Delete Store", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.Yes)
                        {
                            using (SqlConnection sqlConnection = new SqlConnection(connection))
                            {
                                try
                                {
                                    string query;
                                    query = "UPDATE [dbo].[Stores] " +
                                        "SET [Deleted] = 1 " +
                                        "WHERE [dbo].[Stores].[StoreID] = @id";
                                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("@id", Convert.ToInt32(row.Cells["StoreID"].Value.ToString()));
                                    sqlCommand.CommandType = CommandType.Text;
                                    sqlConnection.Open();
                                    sqlCommand.ExecuteNonQuery();
                                    sqlConnection.Close();
                                    MessageBox.Show("Store Deleted!", "Store", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tcFunctionInput.Visible = false;
                                    loadStores();
                                    tslblMode.Text = "STORE MODE";
                                }
                                catch (System.Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        else if (dialog == DialogResult.Cancel)
                        {
                            tslblMode.Text = "STORE MODE";
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //E. Category
            //1. EDIT, DELETE
            //====================================================================//
            //I. EDIT CATEGORY
            else if (tslblMode.Text == "CATEGORY EDIT MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        tbxMainCategory.Text = row.Cells["Main Category"].Value.ToString();
                        tbxSubcategory.Text = row.Cells["Subcategory"].Value.ToString();
                        lblCategoryID.Text = row.Cells["CategoryID"].Value.ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //II. DELETE STORE
            else if (tslblMode.Text == "CATEGORY DELETE MODE")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = this.dgvMain.Rows[e.RowIndex];
                        DialogResult dialog = MessageBox.Show(string.Format("Are you sure you want to delete:\n{0} - {1}, {2}",
                            row.Cells["CategoryID"].Value.ToString(), row.Cells["Main Category"].Value.ToString(), row.Cells["Subcategory"].Value.ToString()),
                            "Delete Category", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.Yes)
                        {
                            using (SqlConnection sqlConnection = new SqlConnection(connection))
                            {
                                try
                                {
                                    //SQL START
                                    string query;
                                    query = "UPDATE [dbo].[Categories] " +
                                        "SET [Deleted] = 1 " +
                                        "WHERE [dbo].[Categories].[CategoryID] = @id";
                                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                                    sqlCommand.Parameters.AddWithValue("@id", Convert.ToInt32(row.Cells["CategoryID"].Value.ToString()));
                                    sqlCommand.CommandType = CommandType.Text;
                                    sqlConnection.Open();
                                    sqlCommand.ExecuteNonQuery();
                                    sqlConnection.Close();
                                    //SQL END
                                    MessageBox.Show("Category Deleted!", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tcFunctionInput.Visible = false;
                                    loadCategories();
                                    tslblMode.Text = "CATEGORY MODE";
                                }
                                catch (System.Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        else if (dialog == DialogResult.Cancel)
                        {
                            tslblMode.Text = "CATEGORY MODE";
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //====================================================================//
        //METHODS
        //====================================================================//
        
        //POPULATE DATAGRIDVIEW
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
                    "[dbo].[Variants].[ProductCode] as [ProductCode], " +
                    "[dbo].[Products].[MeasurementRate] as [Rate of Measure], " +
                    "[dbo].[Variants].[Barcode] as [Bar Code], " +
                    "[dbo].[Variants].[Deleted] as [Variant Deleted], " +
                    "[dbo].[Products].[Deleted] as [Product Deleted] " +
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

        private void loadProductsOnly()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                DataTable dataTable = new DataTable();
                string command = "select " +
                    "[dbo].[Products].[ProductCode] as [ProductCode], " +
                    "[dbo].[Products].[Description] as [Product Description], " +
                    "[dbo].[Products].[UoM], " +
                    "[dbo].[Products].[Weighted], " +
                    "[dbo].[Products].[MeasurementRate] as [Rate of Measure], " +
                    "[dbo].[Products].[Deleted] as [Product Deleted] " +
                    "from [dbo].[Products]";
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
                string command ="SELECT [dbo].[Transactions].[Date] AS [Date], " +
                                "CONCAT([dbo].[Stores].[StoreName], ', ', [Stores].[StoreLocation]) AS [Store Name], " +
                                "[dbo].[Transactions].[InvoiceNumber] AS [Invoice Number], " +
                                "[dbo].[Transactions].[InvoiceTotalAmount] AS [Total Invoice Amount], " +
                                "[dbo].[Transactions].[Saved] AS [Saved] " +
                                "FROM [dbo].[Transactions] " +
                        "INNER JOIN[dbo].[Stores] ON[dbo].[Transactions].[StoreID] = [dbo].[Stores].[StoreID] " +
                        "ORDER BY [Date] DESC";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                dataTable.Load(sqlCommand.ExecuteReader());
                bindingSource.DataSource = dataTable;
                dgvMain.DataSource = bindingSource;
                sqlConnection.Close();
            }
        }

        private void loadCategories()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                DataTable dataTable = new DataTable();
                string command ="SELECT [dbo].[Categories].[CategoryID] AS [CategoryID], "+
                                "[dbo].[Categories].[ParentCategory] AS [Main Category], " +
                                "[dbo].[Categories].[CategoryName] AS [Subcategory] FROM [dbo].[Categories]";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                dataTable.Load(sqlCommand.ExecuteReader());
                bindingSource.DataSource = dataTable;
                dgvMain.DataSource = bindingSource;
                sqlConnection.Close();
            }
        }

        private void loadStores()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                DataTable dataTable = new DataTable();
                string command ="SELECT [dbo].[Stores].[StoreID] AS [StoreID], "+
                                "[dbo].[Stores].[StoreName] AS [Store Name], "+
                                "[dbo].[Stores].[StoreLocation] AS [Store Location] "+
                                "FROM [dbo].[Stores]";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                dataTable.Load(sqlCommand.ExecuteReader());
                bindingSource.DataSource = dataTable;
                dgvMain.DataSource = bindingSource;
                sqlConnection.Close();
            }
        }


        //OTHER METHODS
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

        public string invoiceNumberGenerator()
        {
            Random random = new Random();
            string invoiceNumber = "";
            for (int i = 1; i < 5; i++)
            {
                invoiceNumber += random.Next(0, 9).ToString();
            }
            return invoiceNumber;
        }

        public decimal getSum()
        {
            decimal sum = 0;
            for (int i = 0; i < dgvMain.Rows.Count; ++i)
            {
                sum += Convert.ToDecimal(dgvMain.Rows[i].Cells["Total Price"].Value);
            }
            return sum;
        }
    }
}
