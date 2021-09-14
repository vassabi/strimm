<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacebookTest.aspx.cs" Inherits="StrimmTube.FacebookTest" %>

<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Access-Control-Allow-Origin" content="*"> 
        <script src="jquery/jquery-1.11.1.min.js"></script>
        <script src="jquery/jqueryUi/jquery-ui.min.js"></script>
        <link href="jquery/jqueryUi/jquery-ui.css" rel="stylesheet" />

        <style>
          .ui-progressbar {
            position: relative;
          }
          .progress-label {
            position: absolute;
            left: 50%;
            top: 4px;
            font-weight: bold;
            text-shadow: 1px 1px 0 #fff;
          }
        </style>
    </head>
  <body>
    <!-- 1. The <iframe> (and video player) will replace this <div> tag. -->
    <div id="player"></div>

    <%= new StrimmTube.CorsUpload { }.ToString()%>

    <script>
      // 2. This code loads the IFrame Player API code asynchronously.
      var tag = document.createElement('script');

      tag.src = "https://www.youtube.com/iframe_api";
      var firstScriptTag = document.getElementsByTagName('script')[0];
      firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

      // 3. This function creates an <iframe> (and YouTube player)
      //    after the API code downloads.
      var player;
      function onYouTubeIframeAPIReady() {
        player = new YT.Player('player', {
          height: '390',
          width: '640',
          events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
          }
        });
      }

      // 4. The API will call this function when the video player is ready.
      function onPlayerReady(event) {
        //event.target.playVideo();
          event.target.loadPlaylist('q2ic9w5N52k,eLQJU4seWno,uuvdAEjcj90', 0, 120, "large");
          event.target.setLoop(false);
          event.target.playVideo();
      }

      // 5. The API calls this function when the player's state changes.
      //    The function indicates that when playing a video (state=1),
      //    the player should play for six seconds and then stop.
      var done = false;
      function onPlayerStateChange(event) {
          var url = event.target.getVideoUrl();
          // "https://www.youtube.com/watch?v=gzDS-Kfd5XQ&feature=..."
          var match = url.match(/[?&]v=([^&]+)/);
          // ["?v=gzDS-Kfd5XQ", "gzDS-Kfd5XQ"]
          var videoId = match[1];

          if (event.data == YT.PlayerState.PLAYING) {
              //console.log('videos is playing');
            }
            else if (event.data == YT.PlayerState.BUFFERING) {
               // console.log('videos is buffering');
            }
            else if (event.data == YT.PlayerState.CUED) {
                //console.log('videos is cued');
            }
            else if (event.data == YT.PlayerState.PAUSED) {
               // console.log('videos is paused');
            }
            else if (event.data == YT.PlayerState.ENDED) {
                //console.log('videos is ended');
            }
            else if (event.data == YT.PlayerState.UNSTARTED) {
               // console.log('videos is unstarted');
            }
      }
      function stopVideo() {
        player.stopVideo();
      }

        var key;
        var uploadedFileName;
        var uploadedVideoPath;
        var customVideo;

        function uploadFile() {
            ClearVideoInfo();

            var file = document.getElementById('file').files[0];
            var fd = new FormData();

            uploadedFileName = (new Date).getTime() + '-' + file.name;
            key = stagingFolder + "/" + uploadedFileName;
            uploadedVideoPath = s3UploadUrl + key;

            fd.append('key', key);
            fd.append('acl', 'public-read');
            fd.append('Content-Type', file.type);
            fd.append('AWSAccessKeyId', access_key);
            fd.append('policy', policy);
            fd.append('signature', signature);
            fd.append("file", file);

            var xhr = new XMLHttpRequest();
            xhr.upload.addEventListener("progress", uploadProgress, false);
            xhr.addEventListener("load", uploadComplete, false);
            xhr.addEventListener("error", uploadFailed, false);
            xhr.addEventListener("abort", uploadCanceled, false);

            $("#progressbar").progressbar({ value: 0 });

            //MUST BE LAST LINE BEFORE YOU SEND 
            xhr.open('POST', s3UploadUrl, true);
            xhr.send(fd);
        }

        function uploadProgress(evt) {
            if (evt.lengthComputable) {
                var progressPercent = Math.round(evt.loaded * 100 / evt.total);
                $("#progressbar").progressbar({
                    value: progressPercent
                });
                if (progressPercent < 100) {
                    $("#progressLabel").text(progressPercent + "%");
                }
                else {
                    $("#progressLabel").text("Video uploaded!");
                }
            } else {
                $("#progressbar").progressbar("option", "value", false);
            }
        }

        function uploadComplete(evt) {
            // Submit video for transcoding to S3
            InitializeVideoUpload();
        }

        var GetVideoDurationService = "/WebServices/VideoUploadWebService.asmx/GetVideoDuration";
        var GetVideoPreviewClipService = "/WebServices/VideoUploadWebService.asmx/GetVideoPreviewClip";
        var GetCustomVideoThumbnailService = "/WebServices/VideoUploadWebService.asmx/GetCustomVideoThumbnail";
        var GetVideoThumbnailsService = "/WebServices/VideoUploadWebService.asmx/GetVideoThumbnails";
        var SubmitVideoForTranscodingService = "/WebServices/VideoUploadWebService.asmx/SubmitVideoForTranscoding";
        var InitializeCustomVideoTubeUploadForUser = "/WebServices/VideoUploadWebService.asmx/InitializeCustomVideoTubeUploadForUser"
        var UpdateUploadedVideoService = "/WebServices/VideoUploadWebService.asmx/UpdateUploadedVideo";

        function GetClientTime() {
            var date = new Date();
            var clientTime = date.getDate() + '-' + date.getMonth() + '-' + date.getYear() + '-' + date.getHours() + '-' + date.getMinutes();
            return clientTime;
        }

        function InitializeVideoUpload() {
            $("#progressbar").progressbar({ value: 0 });
            $("#progressLabel").text("Initializing video processing....");

            var clientTime = GetClientTime();

            $.ajax({
                type: "POST",
                url: InitializeCustomVideoTubeUploadForUser,
                data: '{"userId":' + "'" + 2040 + "'" + ',"filename":' + "'" + uploadedFileName + "'" + ',"videoTubeStagingKey":' + "'" + uploadedVideoPath + "'" + ',"clientDateTime":' + "'" + clientTime + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        var value = $("#progressbar").val();
                        $("#progressbar").progressbar({ value: 25 });
                        if (response.d.IsSuccess) {
                            customVideo = response.d.Response;
                            console.log(customVideo);
                        }
                        else {
                            alert(response.d.Message);
                        }
                    }

                    // Retrieve video duration                    
                    SubmitVideoForTranscoding();
                    RetrieveVideoPreviewClip();
                },
                async: false,
                error: function (response) {
                }
            });

            $("#progressbar").progressbar({ value: 25 });
        }

        function RetrieveVideoDuration() {
            $("#progressLabel").text("Retrieving video duration....");
            $.ajax({
                type: "POST",
                url: GetVideoDurationService,
                data: '{"userId":' + "'" + 2040 + "'" + ',"videoFilePath":' + "'" + uploadedVideoPath + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        $("#progressbar").progressbar({ value: 50 });
                        var data = response.d;
                        if (data.IsSuccess) {
                            var duration = data.Response;
                            $("#duration").html(duration);

                            // Need to retrieve video preview clip 
                            RetrieveVideoPreviewClip();
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                },
                async: false,
                error: function (response) {
                }
            });
        }

        function RetrieveVideoPreviewClip() {
            $("#progressLabel").text("Extracting video preview clip....");

            $.ajax({
                type: "POST",
                url: GetVideoPreviewClipService,
                data: '{"videoTubeId":' + customVideo.VideoTubeId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        $("#progressbar").progressbar({ value: 50 });
                        var data = response.d;
                        if (data.IsSuccess) {
                            console.log(data.Response);
                            customVideo.VideoPreviewKey = data.Response;
                            $("#videoClip").html(data.Response);
                        }
                        else {
                            alert(data.Message);
                        }

                        // Need to retrieve video thumbnails
                        RetrieveVideoThumbnails();
                    }
                },
                async: false,
                error: function (response) {
                }
            });
        }

        function RetrieveVideoThumbnails() {
            $("#progressLabel").text("Extracting video thumbnails....");
            $.ajax({
                type: "POST",
                url: GetVideoThumbnailsService,
                data: '{"videoTubeId":' + customVideo.VideoTubeId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        $("#progressbar").progressbar({ value: 100 });
                        $("#progressLabel").text("Video pre-processing complete!");
                        var data = response.d;
                        if (data.IsSuccess) {
                            var videoThumbnails = data.Response;
                            $("#firstThumbnail").html(videoThumbnails[0]);
                            $("#secondThumbnail").html(videoThumbnails[1]);
                            $("#thirdThumbnail").html(videoThumbnails[2]);

                            customVideo.FirstThumbnailKey = videoThumbnails[0];
                            customVideo.SecondThumbnailKey = videoThumbnails[1];
                            customVideo.ThirdThumbnailKey = videoThumbnails[2];
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                },
                async: false,
                error: function (response) {
                }
            });
        }

        function SubmitVideoForTranscoding() {
            $("#progressLabel").text("Submitting video for transcoding....");

            $.ajax({
                type: "POST",
                url: SubmitVideoForTranscodingService,
                data: '{"videoTubeId":' + customVideo.VideoTubeId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        var value = $("#progressbar").val();
                        $("#progressbar").progressbar({ value: 25 });
                        var data = response.d;
                        alert(data.Response);
                    }
                },
                async: false,
                error: function (response) {
                    $("#progressbar").progressbar({ value: 25 });
                }
            });
        }

        function UpdateVideo() {
            $("#progressLabel").text("Updating video....");

            customVideo.Title = $("#txtTitle").val();
            customVideo.Description = $("#txtDescription").val();
            customVideo.Keywords = $("#txtKeywords").val();

            var rrated = $('#chkIsRRated').val();
            if (rrated == 'on') {
                customVideo.IsRRated = true;
            }

            var privacy = $('input[name=rdPrivacy]:checked').val();
            if (privacy == 'private') {
                customVideo.IsPrivate = true;
            }
            else {
                customVideo.IsPrivate = false;
            }

            var jsonCustomVideo = JSON.stringify(customVideo);

            $.ajax({
                type: "POST",
                url: UpdateUploadedVideoService,
                data: '{"videoModel":' + jsonCustomVideo + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        $("#progressLabel").text("Video Updated!");
                        $("#progressbar").progressbar({ value: 100 });
                        var data = response.d;
                        alert(data.Response);
                    }
                },
                async: false,
                error: function (response) {
                    $("#progressLabel").text("Video update failed!");
                    $("#progressbar").progressbar({ value: 100 });
                }
            });
        }

        function uploadFailed(evt) {
            alert("There was an error attempting to upload the file." + evt);
        }

        function uploadCanceled(evt) {
            alert("The upload has been canceled by the user or the browser dropped the connection.");
        }

        function ClearVideoInfo() {
            $("#duration").html("");
            $("#videoClip").html("");
            $("#firstThumbnail").html("");
            $("#secondThumbnail").html("");
            $("#thirdThumbnail").html("");
        }

    </script>

    <form id="corsForm" enctype="multipart/form-data" method="post">
        <label for="file">Select a File to Upload</label><br />
        <input type="file" name="file" id="file"/>
        <input type="button" onclick="uploadFile()" value="Upload" />
        <div id="progressbar" class="ui-progressbar"><div id="progressLabel" class="progress-label">Waiting</div></div>

        <div id="videoInfo">
            <div>Duration: <div id="duration"></div> sec</div><br />
            <div>Video clip: <div id="videoClip"></div></div><br />
            <div>Video Thumbnail #1: <div id="firstThumbnail"></div></div>
            <div>Video Thumbnail #2: <div id="secondThumbnail"></div></div>
            <div>Video Thumbnail #3: <div id="thirdThumbnail"></div></div>
            <input type="text" id="txtTitle" placeholder="Enter video title" />
            <textarea id="txtKeywords" style="height:50px;width:100px;" placeholder="Enter keywords"></textarea>
            <textarea id="txtDescription" style="height:50px;width:100px;" placeholder="Enter video description"></textarea>
            <div id="dPrivacy">
                <input type="radio" name="rdPrivacy" id="rdPrivate" value="private" />Private
                <input type="radio" name="rdPrivacy" id="rdPublic" value="public" checked/>Public
            </div> 
            <input type="checkbox" id="chkIsRRated" />Is R-Rated 
        </div>
        <input type="button" name="Update" id="btnUpdate" onclick="UpdateVideo()"/>          

    </form>

<style>
/* Prevent the text contents of draggable elements from being selectable. */
[draggable] {
  -moz-user-select: none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  user-select: none;
  /* Required to make elements draggable in old WebKit */
  -khtml-user-drag: element;
  -webkit-user-drag: element;
}
.row {
  height: 100px;
  width: 200px;
  border: 2px solid #666666;
  background-color: #ccc;
  margin-right: 5px;
  -webkit-border-radius: 10px;
  -ms-border-radius: 10px;
  -moz-border-radius: 10px;
  border-radius: 10px;
  -webkit-box-shadow: inset 0 0 3px #000;
  -ms-box-shadow: inset 0 0 3px #000;
  box-shadow: inset 0 0 3px #000;
  text-align: center;
  cursor: move;
}
.row header {
  color: #fff;
  text-shadow: #000 0 1px;
  box-shadow: 5px;
  padding: 5px;
  background: -moz-linear-gradient(left center, rgb(0,0,0), rgb(79,79,79), rgb(21,21,21));
  background: -webkit-gradient(linear, left top, right top,
                               color-stop(0, rgb(0,0,0)),
                               color-stop(0.50, rgb(79,79,79)),
                               color-stop(1, rgb(21,21,21)));
  background: -webkit-linear-gradient(left center, rgb(0,0,0), rgb(79,79,79), rgb(21,21,21));
  background: -ms-linear-gradient(left center, rgb(0,0,0), rgb(79,79,79), rgb(21,21,21));
  border-bottom: 1px solid #ddd;
  -webkit-border-top-left-radius: 10px;
  -moz-border-radius-topleft: 10px;
  -ms-border-radius-topleft: 10px;
  border-top-left-radius: 10px;
  -webkit-border-top-right-radius: 10px;
  -ms-border-top-right-radius: 10px;
  -moz-border-radius-topright: 10px;
  border-top-right-radius: 10px;
}
.schedule {
    float:right;
    border: 2px solid #000;
    height: 800px;
}
.container {
    width: 600px;
}
.videos {
    width: 300px;
    float: left;
}
.video {
  height: 100px;
  width: 100px;
  border: 2px solid #666666;
  background-color: #ccc;
  margin-right: 5px;
  -webkit-border-radius: 10px;
  -ms-border-radius: 10px;
  -moz-border-radius: 10px;
  border-radius: 10px;
  -webkit-box-shadow: inset 0 0 3px #000;
  -ms-box-shadow: inset 0 0 3px #000;
  box-shadow: inset 0 0 3px #000;
  text-align: center;
  cursor: move;
}
.video header {
  color: #fff;
  text-shadow: #000 0 1px;
  box-shadow: 5px;
  padding: 5px;
  background: -moz-linear-gradient(left center, rgb(0,0,0), rgb(79,79,79), rgb(21,21,21));
  background: -webkit-gradient(linear, left top, right top,
                               color-stop(0, rgb(0,0,0)),
                               color-stop(0.50, rgb(79,79,79)),
                               color-stop(1, rgb(21,21,21)));
  background: -webkit-linear-gradient(left center, rgb(0,0,0), rgb(79,79,79), rgb(21,21,21));
  background: -ms-linear-gradient(left center, rgb(0,0,0), rgb(79,79,79), rgb(21,21,21));
  border-bottom: 1px solid #ddd;
  -webkit-border-top-left-radius: 10px;
  -moz-border-radius-topleft: 10px;
  -ms-border-radius-topleft: 10px;
  border-top-left-radius: 10px;
  -webkit-border-top-right-radius: 10px;
  -ms-border-top-right-radius: 10px;
  -moz-border-radius-topright: 10px;
  border-top-right-radius: 10px;
}
.row.over {
  border: 2px dashed #000;
}
.video.over {
  border: 2px dashed #000;
}
.schedule.over {
  border: 2px dashed #000;
}
</style>


    <div id="example" class="container">
        <div id="videos">
            <div id="video1" class="video" draggable="true"><header>First Video</header></div>
            <div id="video2" class="video" draggable="true"><header>Second Video</header></div>
        </div>
        <div id="rows" class="schedule">
          <div class="row" draggable="true"><header>Video A</header></div>
          <div class="row" draggable="true"><header>Video B</header></div>
          <div class="row" draggable="true"><header>Video C</header></div>
        </div>
    </div>
      <script>
          var dragSrcEl = null;
          var schedule = null;
          var cols = null;
          var videoEl = null;
          var videos = null;

          function handleDragStart(e) {
              this.style.opacity = '0.4';

              dragSrcEl = this;

              e.dataTransfer.effectAllowed = 'move';
              e.dataTransfer.setData('text/html', this.innerHTML);
          }

          function handleVideoDragStart(e) {
              this.style.opacity = '0.4';

              videoEl = this;

              e.dataTransfer.effectAllowed = 'move';
              e.dataTransfer.setData('text/html', this.innerHTML);
          }

          function handleDragOver(e) {
              if (e.preventDefault) {
                  e.preventDefault(); // Necessary. Allows us to drop.
              }

              e.dataTransfer.dropEffect = 'move';  // See the section on the DataTransfer object.

              return false;
          }

          function handleDragEnter(e) {
              // this / e.target is the current hover target.
              this.classList.add('over');
          }

          function handleDragLeave(e) {
              this.classList.remove('over');  // this / e.target is previous target element.
          }

          function handleDrop(e) {
              // this / e.target is current target element.

              if (e.stopPropagation) {
                  e.stopPropagation(); // stops the browser from redirecting.
              }

              // Don't do anything if dropping the same column we're dragging.
              if (dragSrcEl != null && dragSrcEl != this) {
                  // Set the source column's HTML to the HTML of the column we dropped on.
                  dragSrcEl.innerHTML = this.innerHTML;
                  this.innerHTML = e.dataTransfer.getData('text/html');
              }

              if (videoEl != null && videoEl != this) {
                  var header = videoEl.innerHTML;

                  schedule.innerHTML = schedule.innerHTML + '<div class="row" draggable="true">' + header + '</div>';
              }

              return false;
          }

          function handleDragEnd(e) {
              // this/e.target is the source node.

              [].forEach.call(cols, function (col) {
                  col.classList.remove('over');
                  col.style.opacity = '1';
              });

              [].forEach.call(videos, function (video) {
                  video.classList.remove('over');
                  video.style.opacity = '1';
              });

              cols = document.querySelectorAll('#rows .row');
              updateEventListeneres(cols);

              dragSrcEl = null;
              videoEl = null;
          }

          function updateEventListeneres(items) {
              [].forEach.call(items, function (col) {
                  col.removeEventListener('dragstart', handleDragStart, false);
                  col.removeEventListener('dragenter', handleDragEnter, false);
                  col.removeEventListener('dragover', handleDragOver, false);
                  col.removeEventListener('dragleave', handleDragLeave, false);
                  col.removeEventListener('drop', handleDrop, false);
                  col.removeEventListener('dragend', handleDragEnd, false);

                  col.addEventListener('dragstart', handleDragStart, false);
                  col.addEventListener('dragenter', handleDragEnter, false);
                  col.addEventListener('dragover', handleDragOver, false);
                  col.addEventListener('dragleave', handleDragLeave, false);
                  col.addEventListener('drop', handleDrop, false);
                  col.addEventListener('dragend', handleDragEnd, false);
              });
          }

          cols = document.querySelectorAll('#rows .row');
          updateEventListeneres(cols);

          schedule = document.querySelector('#rows');
          schedule.addEventListener('dragenter', handleDragEnter, false);
          schedule.addEventListener('dragover', handleDragOver, false);
          schedule.addEventListener('dragleave', handleDragLeave, false);
          schedule.addEventListener('drop', handleDrop, false);
          schedule.addEventListener('dragend', handleDragEnd, false);

          videos = document.querySelectorAll('#videos .video');
          [].forEach.call(videos, function (video) {
              video.addEventListener('dragstart', handleVideoDragStart, false);
          });
    </script>
  </body>
</html>
