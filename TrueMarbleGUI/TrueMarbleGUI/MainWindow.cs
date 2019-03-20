using System;
using Gtk;
using TrueMarbleBiz;
using System.ServiceModel;

public partial class MainWindow : Gtk.Window
{
    // server object creation
    ITMBizController m_biz;

    int m_zoom, m_x, m_y;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        try {

            ChannelFactory<ITMBizController> channelFactory;
            NetTcpBinding tcpBind = new NetTcpBinding();

            tcpBind.MaxReceivedMessageSize = System.Int32.MaxValue;
            tcpBind.ReaderQuotas.MaxArrayLength = System.Int32.MaxValue;


            channelFactory = new ChannelFactory<ITMBizController>(tcpBind, "net.tcp://localhost:50002/TMBiz");  // listening for biz tier with the port 50002
            m_biz = channelFactory.CreateChannel();  

            // default properties of a tile when loading an image tile
            this.m_zoom = 4;
            this.m_x = 0;
            this.m_y = 0;

            byte[] imageBuffer = m_biz.LoadTile(m_zoom, m_x, m_y);
            tmImageBox.Pixbuf = new Gdk.Pixbuf(imageBuffer);

        }
        catch(GLib.GException exception)
        {
            Console.WriteLine("Connection failure! " + exception);
        }
        catch (System.Net.Sockets.SocketException exception)
        {
            Console.WriteLine("Connection failure ! " + exception);
        }





    }



    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
{

    Application.Quit();
    a.RetVal = true;
}




    protected void OnMoveWestClicked(object sender, EventArgs e)
    {
        try
        {
            if (m_x != 0)
            {
                m_x = m_x - 1;
                byte[] imagebuffer = m_biz.LoadTile(m_zoom, m_x, m_y);
                tmImageBox.Pixbuf = new Gdk.Pixbuf(imagebuffer);
            }

        }
        catch (Exception exception)
        {
            Console.WriteLine("Moving left failed!: " + exception);
        }
    }

    protected void OnMoveEastClicked(object sender, EventArgs e)
    {
        try
        {
            int xTilesCount = m_biz.GetNumTilesAcross(m_zoom) - 1;

            if (m_x < xTilesCount)
            {
                m_x = m_x + 1;
                byte[] imagebuffer = m_biz.LoadTile(m_zoom, m_x, m_y);
                tmImageBox.Pixbuf = new Gdk.Pixbuf(imagebuffer);

            }
        }
        catch (Exception exception)
        {
            Console.WriteLine("Moving right failed!: " + exception);
        }
    }

    protected void OnMoveNorthClicked(object sender, EventArgs e)
    {
        try {

            int yTilesCount = m_biz.GetNumTilesDown(m_zoom) - 1;

            if(m_y < yTilesCount) {

                m_y = m_y + 1;

                byte[] imageBuffer = m_biz.LoadTile(m_zoom, m_x, m_y);
                tmImageBox.Pixbuf = new Gdk.Pixbuf(imageBuffer);
            }
        } catch(Exception exception) {

            Console.WriteLine("Moving up failed!: " + exception);

        }
    }

    protected void OnMoveSouthClicked(object sender, EventArgs e)
    {
        try { 

            if(m_y != 0) {

                m_y = m_y - 1;

                byte[] imageBuffer = m_biz.LoadTile(m_zoom, m_x, m_y);
                tmImageBox.Pixbuf = new Gdk.Pixbuf(imageBuffer);


            }

        } catch(Exception exception) {

            Console.WriteLine("Failed moving down!" + exception);
        }
    }

    protected void OnZoomInClicked(object sender, EventArgs e)
    {
        try { 
        
            if(m_zoom != 0) {

                m_zoom = m_zoom - 1;

                byte[] imageBuffer = m_biz.LoadTile(m_zoom, m_x, m_y);
                tmImageBox.Pixbuf = new Gdk.Pixbuf(imageBuffer);
            }

        } catch(Exception exception) {

            Console.WriteLine("Failed Zooming in!" + exception);
        }
    }

    protected void OnZoomOut1Clicked(object sender, EventArgs e)
    {
        try
        {
            if (m_zoom < 6)
            {

                m_zoom = m_zoom + 1;

                byte[] imageBuffer = m_biz.LoadTile(m_zoom, m_x, m_y);
                tmImageBox.Pixbuf = new Gdk.Pixbuf(imageBuffer);

            }
        }
        catch (Exception exception)
        {

            Console.WriteLine("Failed Zooming out!" + exception);
        }
    }
}
