using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMap.NET
{
    using System;
    using System.Globalization;
    /// <summary>
    /// the point of coordinates
    /// </summary>
    [Serializable]
    public struct PointLatLngAlt
    {
        public static readonly PointLatLngAlt Empty = new PointLatLngAlt();
        private double lat;
        private double lng;
        private double alt;
        bool NotEmpty;
        public enum PointType { Takeoff, Land, Loiter,None};
        PointType pointType;
        public PointLatLngAlt(double lat, double lng, double alt)
        {
            this.lat = lat;
            this.lng = lng;
            this.alt = alt;
            NotEmpty = true;
            pointType = PointType.None;
        }

        public PointLatLngAlt(PointLatLng Point)
        {
            this.lat = Point.Lat;
            this.lng = Point.Lng;
            this.alt = 0;
            NotEmpty = true;
            pointType = PointType.None;
            
        }
        /// <summary>
        /// returns true if coordinates wasn't assigned
        /// </summary>
        public bool IsEmpty
        {
            get { return !NotEmpty; }
        }
        public PointType Type
        {
            get { return pointType; }
            set { pointType = value;}
        }
        public double Lat
        {
            get { return this.lat; }
            set { this.lat = value; NotEmpty = true;}
        }
        public double Lng
        {
            get { return this.lng; }
            set { this.lng = value; NotEmpty = true; }
        }
        public double Alt
        {
            get { return this.alt; }
            set { this.alt = value; NotEmpty = true; }
        }

        public PointLatLng GetPointLatLng()
        {
            return new PointLatLng(this.lat, this.lng);
        }
        public static PointLatLngAlt operator +(PointLatLngAlt pt, SizeLatLngAlt sz)
        {
            return Add(pt, sz);
        }
        public static PointLatLngAlt operator -(PointLatLngAlt pt, SizeLatLngAlt sz)
        {
            return Subtract(pt, sz);
        }
        public static bool operator ==(PointLatLngAlt left, PointLatLngAlt right)
        {
            return ((left.Lng == right.Lng) && (left.Lat == right.Lat) && (left.Alt == right.Alt));
        }
        public static bool operator !=(PointLatLngAlt left, PointLatLngAlt right)
        {
            return !(left == right);
        }
        public static PointLatLngAlt Add(PointLatLngAlt pt, SizeLatLngAlt sz)
        {
            return new PointLatLngAlt(pt.Lat - sz.HeightLat, pt.Lng + sz.WidthLng, pt.Alt+sz.Alt );
        }
        public static PointLatLngAlt Subtract(PointLatLngAlt pt, SizeLatLngAlt sz)
        {
            return new PointLatLngAlt(pt.Lat + sz.HeightLat, pt.Lng - sz.WidthLng, pt.Alt-sz.Alt);
        }
        public override bool Equals(object obj)
        {
            if(!(obj is PointLatLngAlt))
            {
                return false;
            }
            PointLatLngAlt tf = (PointLatLngAlt)obj;
            return (((tf.Lng == this.Lng) && (tf.Lat == this.Lat) && (tf.Alt == this.Alt)) && tf.GetType().Equals(base.GetType()));
        }
        public void Offset(PointLatLngAlt pos)
        {
            this.Offset(pos.Lat, pos.Lng, pos.Alt);
        }
        public void Offset(double lat, double lng, double alt)
        {
            this.Lng += lng;
            this.Lat -= lat;
            this.Alt += alt;
        }
        public override int GetHashCode()
        {
            return (this.Lng.GetHashCode() ^ this.Lat.GetHashCode());
        }
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Lat={0}, Lng={1}, Alt={2}}}", this.Lat, this.Lng, this.Alt);
        }

        public string ToStringCSV()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}, {1}, {2}", this.Lat, this.Lng, this.Alt);
        }
    }

    public struct SizeLatLngAlt
    {
        public static readonly SizeLatLngAlt Empty;
        private double heightLat;
        private double widthLng;
        private double alt;
        public SizeLatLngAlt(SizeLatLngAlt size)
        {
            this.widthLng = size.widthLng;
            this.heightLat = size.heightLat;
            this.alt = size.alt;
        }
        public SizeLatLngAlt(PointLatLngAlt pt)
        {
            this.heightLat = pt.Lat;
            this.widthLng = pt.Lng;
            this.alt = pt.Alt;
        }
        public SizeLatLngAlt(double heightLat, double widthLng, double alt)
        {
            this.heightLat = heightLat;
            this.widthLng = widthLng;
            this.alt = alt;

        }
        public static SizeLatLngAlt operator+(SizeLatLngAlt sz1, SizeLatLngAlt sz2)
        {
            return Add(sz1, sz2);
        }
        public static SizeLatLngAlt operator-(SizeLatLngAlt sz1, SizeLatLngAlt sz2)
        {
            return Subtract(sz1, sz2);
        }
        public static bool operator==(SizeLatLngAlt sz1, SizeLatLngAlt sz2)
        {
            return ((sz1.WidthLng == sz2.WidthLng) && (sz1.HeightLat == sz2.HeightLat) && (sz1.alt == sz2.alt));
        }
        public static bool operator!=(SizeLatLngAlt sz1, SizeLatLngAlt sz2)
        {
            return !(sz1 == sz2);
        }
        public static explicit operator PointLatLngAlt(SizeLatLngAlt size)
        {
            return new PointLatLngAlt(size.HeightLat, size.WidthLng, size.alt);
        }
        public bool IsEmpty
        {
            get { return ((this.widthLng == 0d) && (this.heightLat == 0d) && (this.alt == 0d)); }
        }
        public double WidthLng
        {
            get{ return this.widthLng; }
            set{ this.widthLng = value; }
        }
        public double HeightLat
        {
            get { return this.heightLat; }
            set { this.heightLat = value; }
        }
        public double Alt
        {
            get { return this.alt; }
            set { this.alt = value; }
        }
        public static SizeLatLngAlt Add(SizeLatLngAlt sz1, SizeLatLngAlt sz2)
        {
            return new SizeLatLngAlt(sz1.HeightLat + sz2.HeightLat, sz1.WidthLng + sz2.WidthLng, sz1.alt + sz2.alt);
        }
        public static SizeLatLngAlt Subtract(SizeLatLngAlt sz1, SizeLatLngAlt sz2)
        {
            return new SizeLatLngAlt(sz1.HeightLat - sz2.HeightLat, sz1.WidthLng - sz2.WidthLng, sz1.alt-sz2.alt);
        }
        public override bool Equals(object obj)
        {
            if(!(obj is SizeLatLngAlt))
            {
                return false;
            }
            SizeLatLngAlt ef = (SizeLatLngAlt) obj;
            return (((ef.WidthLng == this.WidthLng) && (ef.HeightLat == this.HeightLat) && (ef.Alt == this.Alt)) && ef.GetType().Equals(base.GetType()));
        }
        public override int GetHashCode()
        {
            if(this.IsEmpty)
            {
                return 0;
            }
            return (this.WidthLng.GetHashCode() ^ this.HeightLat.GetHashCode());
        }
        public PointLatLngAlt ToPointLatLng()
        {
            return (PointLatLngAlt) this;
        }
        public override string ToString()
        {
            return ("{WidthLng=" + this.widthLng.ToString(CultureInfo.CurrentCulture) + ", HeightLng=" + this.heightLat.ToString(CultureInfo.CurrentCulture) + "}");
        }
        static SizeLatLngAlt()
        {
            Empty = new SizeLatLngAlt();
        }
    }
}

 
