using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.Collections.Generic;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/FullHistory")]
    public class FullHistoryController : Controller
    {
        public class outputeditor
        {
            public Order order { get; set; }
            public DecreeHistroryElementToAppending new_order { get; set; }
        };

        private Repository repository;
        OrderWorker m_order_worker;

        public FullHistoryController(Repository repository)
        {
            this.repository = repository;
            m_order_worker = new OrderWorker(repository);
        }

        [HttpGet("Structure/{id}")]
        public IEnumerable<Order> PrintOrderhistoryByStructures([FromRoute] int id)
        {
            m_order_worker.Initializ_user(Request.Cookies[Keys.COOKIES_SESSION]);
            m_order_worker.Initializ_structure(id);

            return m_order_worker.get_orders();
        }

        [HttpGet("Position/{id}")]
        public IEnumerable<Order> PrintOrderhistoryByPositions([FromRoute] int id)
        {
            m_order_worker.Initializ_user(Request.Cookies[Keys.COOKIES_SESSION]);
            m_order_worker.Initializ_position(id);

            return m_order_worker.get_orders();
        }

        [HttpPost("Structure/AddDecree")]
        public IEnumerable<Order> AddOrderhistory([FromBody] DecreeHistroryElementToAppending decree_to_append)
        {
            m_order_worker.Initializ_user(Request.Cookies[Keys.COOKIES_SESSION]);
            m_order_worker.Initializ_history_decree_by_structure(decree_to_append);

            return m_order_worker.get_orders();
        }

        [HttpPost("Structure/RemoveDecree/{id}")]
        public IEnumerable<Order> RemoveOrderhistory([FromRoute] int id, [FromBody] Order order)
        {
            m_order_worker.Initializ_user(Request.Cookies[Keys.COOKIES_SESSION]);
            m_order_worker.Initialize_order_structureId_for_remove(order, id);

            return m_order_worker.get_orders();
        }

        [HttpPost("Structure/EditDecree")]
        public IEnumerable<Order> EditOrderhistory([FromBody] FullHistoryController.outputeditor input)
        {
            m_order_worker.Initializ_user(Request.Cookies[Keys.COOKIES_SESSION]);
            m_order_worker.Initializ_history_decree_by_edit(input.order, input.new_order);

            return m_order_worker.get_orders();
        }
    }
}