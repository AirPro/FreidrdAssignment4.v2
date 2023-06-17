using OrderDetailsMaintenance.Models.DataLayer;
using System.Xml.Linq;

namespace OrderDetailsMaintenance
{
    public partial class frmCustomerMaintenance : Form
    {

        public NorthwindContext _context;
        public Customer _orderOptions;


        public frmCustomerMaintenance()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Bob Freid: See explanation below.
        private void frmCustomerMaintenance_Load(object sender, EventArgs e)
        {
            // This code would retreive the first entry from the Northwind database.
            // _context = new NorthwindContext();
            // _orderOptions = _context.Customers.First();
        }

        // Bob Freid: This block captures the information entered into the txtCustomerId text box by the User
        // And if the entry exists retreives the information from the Northwind database
        // And displays it the form or throws an error and display the Message Box

        private void btnFind_Click(object sender, EventArgs e)
        {

            string customerId = txtCustomerId.Text;
            _context = new NorthwindContext();
            Customer? customer = _context.Customers.Where(i => i.CustomerId == customerId).FirstOrDefault();
            
            try
            {
                // var selectedCustomer = _orderOptions;
                if (customer == null)
                {
                    MessageBox.Show("No customer found with this ID. " +
                        "Please try again.", "Customer Not Found");

                }
                else
                {
                    txtContact.Text = customer.ContactTitle;
                    txtAddress.Text = customer.Address;
                    txtCity.Text = customer.City;
                    txtCountry.Text = customer.Country;
                }
            }
            catch (DataAccessException ex)
            {
                HandleDataAccessException(ex);
            }

        }

        private void HandleDataAccessException(DataAccessException ex)
        {
            // if it's a concurrency error, clear controls or display updated data 
            if (ex.IsConcurrencyError)
            {
                if (ex.IsDeleted)
                    ClearControls();
                else
                    DisplayCustomer();
            }

            // for all errors, display error message and error type
            MessageBox.Show(ex.Message, ex.ErrorType);
        }

        public void DisplayCustomer()
        {

        }

        public void ClearControls()
        {
            txtCustomerId.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";
            // btnSave.Enabled = false;
            // btnExit.Enabled = false;
            txtCustomerId.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;
            _context = new NorthwindContext();
            Customer? customer = _context.Customers.Where(i => i.CustomerId == customerId).FirstOrDefault();

            if (customer != null)
            {
                customer.ContactName = txtContact.Text;
                customer.Address = txtAddress.Text;
                customer.City = txtCity.Text;
                customer.Country = txtCountry.Text;
                _context.Customers.Update(customer);
                _context.SaveChanges();
            }
            else
            {
                MessageBox.Show("No customer found with this ID. " +
                        "Please try again.", "Customer Not Found");
            }
        }
    }
}