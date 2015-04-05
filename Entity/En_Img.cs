using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class En_Img
    {
        private Guid imgID;//图片ID

        public Guid ImgID
        {
            get { return imgID; }
            set { imgID = value; }
        }

        private Guid uID;//作者ID

        public Guid UID
        {
            get { return imgID; }
            set { imgID = value; }
        }

        private string imgName;//图片名称

        public string ImgName
        {
            get { return imgName; }
            set { imgName = value; }
        }

        private DateTime imgTime;//拍摄时间

        public DateTime ImgTime
        {
            get { return imgTime; }
            set { imgTime = value; }
        }

        private string imgLocation;//拍摄地点

        public string ImgLocation
        {
            get { return imgLocation; }
            set { imgLocation = value; }
        }
        private string imgPath;//图片路径

        public string ImgPath
        {
            get { return imgPath; }
            set { imgPath = value; }
        }

        private int type;//图片类型 1 照片组；2 故事组

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private int sortOrder;//故事组序号

        public int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        private string title;//图片标题

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;//图片描述

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int views;//浏览次数

        public int Views
        {
            get { return views; }
            set { views = value; }
        }

        private int votes;//投票数

        public int Votes
        {
            get { return votes; }
            set { votes = value; }
        }

        private int state;

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        public En_Img() { }

        public En_Img(Guid _imgid, Guid _uid, string _imgname, string _imgpath, DateTime _imgTime, string _imgLocation, int _type, int _sortorder, string _title, string _description, int _views, int _votes, int _state)
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
            views = _views;
            votes = _votes;
            state = _state;
        }
    }
}
