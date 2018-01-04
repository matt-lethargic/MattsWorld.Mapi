using System;
using System.Windows.Forms;
using MattsWorld.Mapi.Data;

namespace MattsWorld.Mapi.Manager
{
    public partial class ManagerForm : Form
    {
        private readonly MappingManager _mappingManager;

        public ManagerForm()
        {
            InitializeComponent();

            IRepository repository = new BlobRepository();
            _mappingManager = new MappingManager(repository);
        }

        private void CreateMappingMenuItem_Click(object sender, EventArgs e)
        {
            var createMappingForm = new CreateMappingForm(_mappingManager);
            createMappingForm.MdiParent = this;
            createMappingForm.Show();
        }
    }
}
