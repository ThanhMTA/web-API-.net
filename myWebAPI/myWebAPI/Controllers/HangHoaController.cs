using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myWebAPI.Models;

namespace myWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id)
                           );
                if (hanghoa == null)
                {
                    return NotFound();

                }
                return Ok(hanghoa);
            }
            catch
            {
                return BadRequest();
            }
           
        }

        [HttpPost] // them 
        public IActionResult Create (HangHoaVm hanghoaVm)
        {
            var hanghoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hanghoaVm.TenHangHoa,
                DonGia = hanghoaVm.DonGia
            };
            hangHoas.Add(hanghoa);
            return Ok(new
            {
                Success = true, Data = hanghoa
            });
        }
        [HttpPut]// sua 
        public IActionResult Edit(string id, HangHoa hanghoaEdit) {
            try
            {
                var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id)
                           );
                if (hanghoa == null)
                {
                    return NotFound();

                }
                if (id != hanghoa.MaHangHoa.ToString())
                {
                    return BadRequest();

                }
                hanghoa.TenHangHoa = hanghoaEdit.TenHangHoa;
                hanghoa.DonGia = hanghoaEdit.DonGia;
                return Ok(hanghoa);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Remove (string id)
        {
            try
            {
                var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id)
                           );
                if (hanghoa == null)
                {
                    return NotFound();

                }
                hangHoas.Remove(hanghoa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
