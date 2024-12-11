using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Models
{
    public enum RoomType
    {
        Single,
        Double,
        Suite
    }

    public class Room
    {
        public int RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public int Size { get; set; }
        public bool IsBooked { get; set; }
        public bool IsActive { get; set; }

        public Room(int roomNumber, RoomType type, int size, bool isBooked, bool isActive)
        {
            RoomNumber = roomNumber;
            Type = type;
            Size = size;
            IsBooked = isBooked;
            IsActive = isActive;
        }
    }
}
