using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVideoAPI.Models;
//using System;
//using Microsoft.Extensions.Logging;



namespace MyVideoAPI.Controllers
{
  [Route("api/v1/[controller]")]  
    [ApiController]  
      public class videosController : ControllerBase  
    {  
      private MyVideoApiContext myDbContext;  

      public videosController(MyVideoApiContext context)  
      {  
        myDbContext = context;  
      }  

      //GET : api/v1/videos
      [HttpGet]
      public IList<VideoModel> Get()
      {  
        return (this.myDbContext.MyVideos.ToList());
      }  

      //GET : api/v1/videos/{id}
      [HttpGet("{id}")]
      public async Task<ActionResult<VideoModel>> GetItem(long id)
      {
        var myvideo = await myDbContext.MyVideos.FindAsync(id);

        if (myvideo == null)
        {
          return NotFound();
        }

        return myvideo;
      }

      //POST : api/v1/videos
      [HttpPost]
      public async Task<ActionResult<VideoModel>> PostItem(VideoModel videoModel)
      {
        myDbContext.MyVideos.Add(videoModel);
        await myDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = videoModel.ID }, videoModel);
      }

      //PUT : api/v1/videos
      [HttpPut("{id}")]
      public async Task<IActionResult> PutItem(long id, VideoModel myvideo)
      {
        if (id != myvideo.ID)
        {
          return BadRequest();
        }

        myDbContext.Entry(myvideo).State = EntityState.Modified;

        try
        {
          await myDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!VideoModelExists(id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }

        return NoContent();
      }

      // DELETE: api/v1/videos/{id}
      [HttpDelete("{id}")]
      public async Task<ActionResult<VideoModel>> DeleteItem(int id)
      {
        var myvideo = await myDbContext.MyVideos.FindAsync(id);
        if (myvideo == null)
        {
          return NotFound();
        }

        myDbContext.MyVideos.Remove(myvideo);
        await myDbContext.SaveChangesAsync();

        return myvideo;
      }

      private bool VideoModelExists(long id)
      {
        return myDbContext.MyVideos.Any(e => e.ID == id);
      }
    }  
}
