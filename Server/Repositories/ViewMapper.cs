using ModelProvider;
using ModelProvider.Models;
using ModelProvider.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repositories
{ 

    public static class ViewMapper
    {
        public static AutoView GetViewModell(Auto a)
        {
            AutoView av = new AutoView();
            if (a != null)
            {
               
                av.Brand = a.Brand;
                av.Model = a.Model;
                av.Id = a.Id;
                av.LicencePlate = a.LicencePlate;
                av.Client = GetViewModell(a.Client);
            }
          
            return av;
        }

        public static ClientView GetViewModell(Client c)
        {
            ClientView cv = new ClientView();
            if (c != null)
            {
                cv.Email = c.Email;
                cv.Name = c.Name;
                cv.PhoneNumber = c.PhoneNumber;
                cv.Id = c.Id;     
            }        
            return cv;
        }

        public static BonusView GetViewModell(Bonus b)
        {
            BonusView bv = new BonusView();
            if (b != null)
            {
                bv.Name = b.Name;
                bv.Price = b.Price;
                bv.BonusRepairs = b.BonusRepairs?.Select(br => GetViewModell(br)).ToList(); 
            }
            return bv;
        }
     
        public static BonusRepairView GetViewModell(BonusRepair br)
        {
            BonusRepairView brv = new BonusRepairView();
            if (br != null)
            {
                brv.BonusName = br.BonusName;
                brv.RepairID = br.RepairID; 
            }
            return brv;      
        }
        public static ManagerView GetViewModell(Manager m)
        {
            ManagerView mv = new ManagerView();
            if (m != null)
            {
                mv.Name = m.Name;
                mv.PhoneNumber = m.PhoneNumber;
                mv.Id = m.Id;
                mv.User = GetViewModell(m.User);  
            }           
            return mv;
        }
        public static RepairLogView GetViewModell(RepairLog rl)
        {
            RepairLogView rlv = new RepairLogView();
            if (rl != null)
            {
                rlv.Id = rl.Id;
                rlv.Date = rl.Date;
                rlv.Description = rl.Description;
                rlv.Repair = GetViewModell(rl.Repair);
                rlv.TechnicianId = rl.TechnicianId;            
            }
            return rlv;
        }
        public static RepairTechnicianView GetViewModell(RepairTechnician rt)
        {
            RepairTechnicianView rtv = new RepairTechnicianView();
            if (rt != null)
            {
                rtv.TechnicianId = rt.TechnicianId;
                rtv.RepairID = rt.RepairID; 
            }
            return rtv;
        }
        public static RepairView GetViewModell(Repair r)
        {
            RepairView rv = new RepairView();
            if (r != null)
            {
                rv.Auto = GetViewModell(r.Auto);
                rv.BonusRepairs = r.BonusRepairs?.Select(br => GetViewModell(br)).ToList();
                rv.Description = r.Description;
                rv.Id = r.Id;
                rv.Manager = GetViewModell(r.Manager);
                rv.Price = r.Price;
                rv.RepairTechnicians = r.RepairTechnicians?.Select(br => GetViewModell(br)).ToList();
                rv.State = r.State;           
            }  
            return rv;
        }
        public static TechnicianView GetViewModell(Technician t)
        {
            TechnicianView tv = new TechnicianView();
            if (t != null)
            {
               
                tv.Id = t.Id;
                tv.Name = t.Name;
                tv.PhoneNumber = t.PhoneNumber;
                tv.RepairTechnicians = t.RepairTechnicians?.Select(br => GetViewModell(br)).ToList();
                tv.User = GetViewModell(t.User);    
            }         
            return tv;
        }
        public static UserView GetViewModell(User u)
        {
            UserView uv = new UserView();
            if (u != null)
            {
                uv.Id = u.Id;
                uv.isManager = u.isManager;
                uv.Password = u.Password;
                uv.Username = u.Username;
                uv.Email = u.Email;
            }        
            return uv;
        }          

    }
   
}
