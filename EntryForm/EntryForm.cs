namespace PetPhoneBook
{
    public partial class EntryForm : Form
    {
        PhoneBook pbook;
        Entry entry;

        private void UpdateFields()
        {
            label5.Text = entry.Name + ' ' + entry.Surname;
            textBox1.Text = entry.Name;
            textBox2.Text = entry.Surname;
            textBox5.Text = entry.Phone.Prefix.ToString();
            textBox3.Text = entry.Phone.Number;
            textBox6.Text = entry.Email?.Address;
            textBox4.Text = entry.Email?.Domen;
        }

        public event Action<Entry>? OnEntryUpdated;

        public EntryForm(Entry entry)
        {
            InitializeComponent();
            pbook = new PhoneBook();
            this.entry = entry;
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            UpdateFields();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

                pbook.Update(entry.Phone.ToString(), new_entry);
                entry = new_entry;
            }
            catch (ValidationException exc)
            {
                MessageBox.Show(exc.Message);
                UpdateFields();
                return;
            }
            catch
            {
                MessageBox.Show("unable to update entry");
                UpdateFields();
                return;
            }

            OnEntryUpdated?.Invoke(entry);
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
