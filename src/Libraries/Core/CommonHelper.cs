using System;
using System.Collections.Generic;
using System.Text;
using Core.Infrastructure;

namespace Core
{
    public partial class CommonHelper
    {
        #region Properties

        /// <summary>
        /// Gets or sets the default file provider
        /// </summary>
        public static IRemFileProvider DefaultFileProvider { get; set; }

        #endregion
    }
}
