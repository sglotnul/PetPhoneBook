namespace PetPhoneBook
{
    public partial class MainForm : Form
    {
        private PhoneBook pbook;
        private List<string> phone_numbers;
        private void UpdateEntries()
        {
            listBox1.Items.Clear();
            foreach (PreviewEntry entry in pbook.GetPreviews())
            {
                phone_numbers.Add(entry.Phone);
                listBox1.Items.Add(entry.Name);
            }
        }

        private void UpdateEntries(string search)
        {
            listBox1.Items.Clear();
            foreach (PreviewEntry entry in pbook.GetPreviews(search))
            {
                phone_numbers.Add(entry.Phone);
                listBox1.Items.Add(entry.Name);
            }
        }

        public MainForm()
        {
            InitializeComponent();
            pbook = new PhoneBook();
            phone_numbers = new List<string>();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateEntries();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index != -1)
            {
                var result = pbook.getByPhoneNumber(phone_numbers[index]);
                if (result == null)
                {
                    MessageBox.Show("unable to find entry");
                }
                else
                {
                    EntryForm f = new EntryForm((Entry)result);
                    f.OnEntryUpdated += (entry) => {
                        phone_numbers[index] = entry.Phone.ToString();
                        listBox1.Items[index] = entry.Name + ' ' + entry.Surname;
                        f.Close();
                    };
                    f.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index != -1)
            {
                try
                {
                    pbook.Delete(phone_numbers[index]);
                    phone_numbers.RemoveAt(index);
                    listBox1.Items.RemoveAt(index);
                }
                catch
                {
                    MessageBox.Show("unable to delete entry");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertForm f = new InsertForm();
            f.OnEntryInserted += (entry) => {
                phone_numbers.Add(entry.Phone.ToString());
                listBox1.Items.Add(entry.Name + ' ' + entry.Surname);
                f.Close();
            };
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(s))
            {
                UpdateEntries(s);
            }
            else UpdateEntries();
        }
    }
}
