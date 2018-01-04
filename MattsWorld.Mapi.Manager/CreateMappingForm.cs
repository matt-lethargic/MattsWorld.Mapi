using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MattsWorld.Mapi.Domain;

namespace MattsWorld.Mapi.Manager
{
    public partial class CreateMappingForm : Form
    {
        private readonly MappingManager _mappingManager;
        private readonly List<MapiMap> _maps;

        public CreateMappingForm(MappingManager mappingManager)
        {
            InitializeComponent();

            _mappingManager = mappingManager ?? throw new ArgumentNullException(nameof(mappingManager));
            _maps = new List<MapiMap>();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            MapiMapping mapiMapping = new MapiMapping(_maps);

            foreach (DataGridViewRow row in MapsDataGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    string from = row.Cells["From"].Value.ToString();
                    string type = row.Cells["Type"].Value.ToString();
                    string to = row.Cells["To"].Value.ToString();

                    string meta = null;
                    if(row.Cells["Meta"].Value != null)
                        meta = row.Cells["Meta"].Value.ToString();

                    mapiMapping.AddMap(new MapiMap(from, type, to, meta));
                }
            }

            await _mappingManager.Save(mapiMapping);
        }
        
    }
}
