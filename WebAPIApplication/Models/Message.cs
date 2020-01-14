using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WebAPIApplication.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public string CreationDate { get; set; } = DateTime.Now.ToString();
        public string HostName { get; set; } = Dns.GetHostName().ToString();
        public string HostIP { get; set; } = Dns.GetHostAddresses(Dns.GetHostName()).Where(address => address.AddressFamily == AddressFamily.InterNetwork).First().ToString();
    }

    //состояния сортировки
    public enum SortState
    {
        IdAsc,//Id по возрастанию
        IdDesc,//Id по убыванию
        CreationDateAsc,//дата по возрастанию
        CreationDateDesc,//дата по убыванию
        HostNameAsc,//имя хоста по возрастанию
        HostNameDesc,//имя хоста по убыванию
        HostIPAsc,//IP по возрастанию
        HostIPDesc//IP по убыванию
    }
}
