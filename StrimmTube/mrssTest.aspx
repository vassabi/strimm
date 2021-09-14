<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mrssTest.aspx.cs" Inherits="StrimmTube.mrssTest" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <title></title>
    <link href="Flowplayer7/skin/skin.css" rel="stylesheet" />
	<link href="css/flowplayer.override.css" rel="stylesheet" />
    <script src="jquery/jquery-1.11.1.min.js"></script>
    <script src="jquery/jquery-migrate-1.2.1.min.js"></script>
    <script src="Flowplayer7/flowplayer.js"></script>
    <script src="Flowplayer7/cc-button-7.0.0.js"></script>
    <script src="Flowplayer7/settingsmenu-7.0.0.js"></script>
    <script src="Flowplayer7/vimeo-7.0.0.js"></script>
    <script src="Flowplayer7/youtube-7.0.0.js"></script>
    <script src="Flowplayer7/dailymotion-7.0.0.js"></script>

</head>
<body>
    <form id="form1" runat="server">
      <div id="player"></div>

    <script>
        var items = [];
        $(document).ready(function () {
            InitPlayer();
            //$.ajax({
            //    url: 'fpPlaylist.xml',
            //    dataType: 'xml',
            //    async:false,
            //    success: function (data) {
                    

            //        var newPlaylist = [];
            //            var $xml = $(data);
            //            $xml.find("item").each(function () {
            //                var $this = $(this),
            //                    item = {
            //                        title: $this.find("title").text(),
            //                        link: $this.find("link").text(),
            //                        description: $this.find("description").text(),
            //                        pubDate: $this.find("pubDate").text(),
            //                        category:$this.find("category").text(),
            //                        url: $this.find("enclosure").attr("url"),
            //                        author: $this.find("author").text(),
            //                        duration: $this.find("media\\:content").attr("duration"),
            //                        videoUrl: $this.find("media\\:content").attr("url"),
            //                        type: $this.find("media\\:content").attr("type"),
            //                    };

            //                // ADD IT TO THE ARRAY
            //                items.push(item);
                            
                           
    

            //                if (items && items.length > 0) {
            //                    $.each(items, function (i, d) {
            //                        //var playlistItem = [{ mp4: d.VideoKey }];
            //                        var typeValue =d.type;
            //                        var srcValue = "";
            //                        // var startTime = (d.PlayerPlaybackStartTimeInSec).toFixed()
                                    
            //                        //var ext = d.ProviderVideoId.substr((d.ProviderVideoId.lastIndexOf('.') + 1));
            //                        //typeValue = "video/" + ext;
            //                        srcValue = d.videoUrl.slice(d.videoUrl.indexOf(':') + 1); 
                                  
            //                       var playlistItem = { sources: [{ type: typeValue, src: d.videoUrl.slice(d.videoUrl.indexOf(':') + 1)}] };
            //                       console.log(playlistItem)
            //                        newPlaylist.push(playlistItem);
           
            //                    });
            //                }
                           
            //                    });
            //            // end of each()
            //            console.log(JSON.stringify(newPlaylist));
                        
            //            InitPlayer(newPlaylist);
                    
            //    },
            //    error: function (data) {
            //        console.log('Error loading XML data');
            //    }
            //});
            
        });
        function InitPlayer(mrssPlayList)
        {
           
         
                
     

        
                var container = document.getElementById("player");

            // install flowplayer into selected container
                flowplayer(container, {
                    bufferTime: 5,
                    autoplay: true,
                    mute: false,
                    splash: false,
                    clip: {
                        sources: [

                            { type: "video/youtube" },
                            { src: "https://www.youtube.com/watch?v=Cjb2aCqxOvU" }
                        ]
                    }
                  //  playlist: mrssPlayList
                });
        }
       
    </script>
        
    </form>
</body>
</html>
