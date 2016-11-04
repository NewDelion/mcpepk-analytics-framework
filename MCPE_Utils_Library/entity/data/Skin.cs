using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.entity.data
{
    public class Skin
    {
        public static int SINGLE_SKIN_SIZE = 64 * 32 * 4;
        public static int DOUBLE_SKIN_SIZE = 64 * 64 * 4;

        public static string MODEL_STEVE = "Standard_Steve";
        public static string MODEL_ALEX = "Standard_Alex";

        private byte[] data = new byte[SINGLE_SKIN_SIZE];
        private string model;

        public Skin(byte[] data)
        {
            this.setData(data);
            this.setModel(MODEL_STEVE);
        }

        public Skin(byte[] data, string model)
        {
            this.setData(data);
            this.setModel(model);
        }

        public Skin(string base64)
        {
            this.setData(Convert.FromBase64String(base64));
            this.setModel(MODEL_STEVE);
        }

        public Skin(string base64, string model)
        {
            this.setData(Convert.FromBase64String(base64));
            this.setModel(model);
        }

        public byte[] getData()
        {
            return data;
        }

        public string getModel()
        {
            return model;
        }

        public void setData(byte[] data)
        {
            if (data.Length != SINGLE_SKIN_SIZE && data.Length != DOUBLE_SKIN_SIZE)
                throw new ArgumentException("Invalid skin");
            this.data = data;
        }

        public void setModel(string model)
        {
            if (model == null || model.Trim() == "")
                model = MODEL_STEVE;
            this.model = model;
        }

        public bool isValid()
        {
            return this.data.Length == SINGLE_SKIN_SIZE || this.data.Length == DOUBLE_SKIN_SIZE;
        }
    }
}
