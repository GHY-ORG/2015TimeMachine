using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class En_showImg
    {
        private string uName;//作者昵称

        public string UName
        {
            get { return uName; }
            set { uName = value; }
        }

        private Guid imgID;//图片的id

        public Guid ImgID
        {
            get { return imgID; }
            set { imgID = value; }
        }

        private string imgPath;//图片路径

        public string ImgPath
        {
            get { return imgPath; }
            set { imgPath = value; }
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

        public En_showImg() { }

        public En_showImg(string _uname, Guid _imgID, string _imgPath, int _views, int _votes)
        {
            uName = _uname;
            imgID = _imgID;
            imgPath = _imgPath;
            views = _views;
            votes = _votes;
        }
    }
}
