namespace PetPhoneBook
{
    public partial class InsertForm : Form
    {
        PhoneBook pbook;
        public InsertForm()
        {
            InitializeComponent();
            pbook = new PhoneBook();
        }
        public event Action<Entry>? OnEntryInserted;

        private void button1_Click(object sender, EventArgs e)
        {
            Entry entry;
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text)) throw new ValidationException("invalid name");

                string name = textBox1.Text.Trim();
                string surname = textBox2.Text.Trim();
                string email_a = textBox6.Text.Trim();
                string email_d = textBox4.Text.Trim();
                Entry new_entry = new Entry
                (
                    new PhoneNumber(textBox5.Text, textBox3.Text),
                    name,
                    surname,
                    (string.IsNullOrEmpty(email_a) && string.IsNullOrEmpty(email_d)) ? null : new EmailAddress(email_a, email_d)
                );

                pbook.Insert(new_entry);
                entry = new_entry;
            }
            catch (ValidationException exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }
            catch
            {
                MessageBox.Show("unable to insert entry");
                return;
            }

            OnEntryInserted?.Invoke(entry);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox3_KeyPress(sender, e);
        }
    }
}
