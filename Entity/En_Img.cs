using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class En_Img
    {
        private Guid imgID;

        public Guid ImgID
        {
            get { return imgID; }
            set { imgID = value; }
        }

        private Guid uID;

        public Guid UID
        {
            get { return imgID; }
            set { imgID = value; }
        }

        private string imgName;

        public string ImgName
        {
            get { return imgName; }
            set { imgName = value; }
        }

        private DateTime imgTime;

        public DateTime ImgTime
        {
            get { return imgTime; }
            set { imgTime = value; }
        }

        private string imgLocation;

        public string ImgLocation
        {
            get { return imgLocation; }
            set { imgLocation = value; }
        }
        private string imgPath;

        public string ImgPath
        {
            get { return imgPath; }
            set { imgPath = value; }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private int sortOrder;

        public int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int state;

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        public En_Img() { }

        public En_Img(Guid _imgid, Guid _uid, string _imgname, string _imgpath, DateTime _imgTime, string _imgLocation, int _type, int _sortorder, string _title, string _description, int _state)
        {
            imgID = _imgid;
            uID = _uid;
            imgName = _imgname;
            imgLocation = _imgpath;
            imgTime = _imgTime;
            imgLocation = _imgLocation;
            type = _type;
            sortOrder = _sortorder;
            title = _title;
            description = _description;
            state = _state;
        }
    }
}
