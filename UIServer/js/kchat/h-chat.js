    var socketIOurl = "http://chat.mhisham.net";
    // var socketIOurl = "http://localhost:8800";
    var newMsgSound = new Audio("/sound/voice_on.ogg");
    newMsgSound.loop = false;
    var client_id = "";
    var client_name = "";
    var socket;

var chatboxManager = function () {
        var a = function (a) { $.extend(chatbox_config, a) },
        b = function (a) { },
        c = function () { return (chatbox_config.width + chatbox_config.gap) * showList.length },
        d = function (a) {
            var b = showList.indexOf(a);
            if (-1 != b) {
                showList.splice(b, 1),
                diff = chatbox_config.width + chatbox_config.gap;
                for (var c = b; c < showList.length; c++) offset = $("#" + showList[c]).chatbox("option", "offset"),
                $("#" + showList[c]).chatbox("option", "offset", offset - diff)
            } else alert("NOTE: Id missing: " + a)
        },
        o = function (id, chatElement) {
            console.log('chatHistory', chatElement);
            var scope = angular.element($("#chatUsers")).scope();
            var socketId = scope.users[id].socket_id;
            socket.emit('chat_history', { 'roomId': socketId },
            function (data) {
                // console.log('history',data);
                $('#' + id).chatbox("option", "boxManager").clearAllMessages();
                for (var i in data) {
                    $('#' + id).chatbox("option", "boxManager").addHistory((data[i].ownerId == client_id ? 'Me' : data[i].owner), data[i].message);
                }
            });
            // $("#"+id).chatbox("option","boxManager").addMsg('','<a href="#" class="old_messages" data-id="'+id+'" >View old Messages</a>');
        },
        e = function (a, b, e) { var g = showList.indexOf(a), h = boxList.indexOf(a); if (-1 != g); else if (-1 != h) { $("#" + a).chatbox("option", "offset", c()); var i = $("#" + a).chatbox("option", "boxManager"); i.toggleBox(), showList.push(a) } else { var j = document.createElement("div"); j.setAttribute("id", a), $(j).chatbox({ "id": a, "user": b, "title": '<i title="' + b.status + '"></i>' + b.first_name + " " + b.last_name, "hidden": !1, "offset": c(), "width": chatbox_config.width, "status": b.status, "alertmsg": b.alertmsg, "alertshow": b.alertshow, "messageSent": f, "boxClosed": d, "getHistory": o }), boxList.push(a), showList.push(a), nameList.push(b.first_name) } },

        f = function (toId, b, message) {
            //Send new message to a with msg c
            // console.log('a',a);console.log('b',b);console.log('c',c);
            //wael Call after Click Enter 
            var scope = angular.element($("#chatUsers")).scope();
            scope.$apply(function () {
                scope.SendMessage(toId, message);
            });
            $("#" + toId).chatbox("option", "boxManager").addMsg("Me", message);
        };
        return { "init": a, "addBox": e, "delBox": b, "dispatch": f }
    }();

function startChat(username, displayName, token) {
    //Client Data
    var token = sessionStorage.PublicKey;
    client_id = username;

    client_name = displayName;
    var room = "cha2";


   
    //$("a[data-chat-id]:not(.offline)").on('click',function(a,b){var c=$(this),d=c.attr("data-chat-id"),e=c.attr("data-chat-fname"),f=c.attr("data-chat-lname"),g=c.attr("data-chat-status")||"online",h=c.attr("data-chat-alertmsg"),i=c.attr("data-chat-alertshow")||!1;chatboxManager.addBox(d,{"title":"username"+d,"first_name":e,"last_name":f,"status":g,"alertmsg":h,"alertshow":i}),a.preventDefault()});

    var windowFocus = false;
    function showNotification(theBody, theIcon, theTitle) {
        if (!("Notification" in window)) {
            return;
        }
        var options = {
            body: theBody,
            icon: theIcon
        }
        var n = new Notification(theTitle, options);
        n.onclick = function () {
            $(window).focus();
            n.close();
        };
        //Close Message after 8 sec.
        setTimeout(function () { n.close(); }, 8000);
    }


    $(document).ready(function () {

        $(window).focus(function () {
            // console.log('focus');
            windowFocus = true;
        });

        $(window).blur(function () {
            // console.log('blur');
            windowFocus = false;
        });
        if (!("Notification" in window)) {
            console.log("Browser doesn't Support Desktop Notifications");
        } else {
            if (Notification.permission !== "granted") {
                Notification.requestPermission().then(function (result) {
                    //   console.log('r',result);
                    if (result == 'granted') {
                        showNotification('Now when you receive new message you will get notification like this', 'img/chat.png', 'Notifications Activated');
                    }
                });
            }
        }

        (function askName() {
            if (!client_name) {
                if (sessionStorage.clientName) {
                    client_name = sessionStorage.clientName;
                } else {
                    client_name = prompt("Please Enter your name", "");
                }
                if (!client_name) {
                    askName();
                } else {
                    sessionStorage.clientName = client_name;
                }
            }
        })();
        $('#client_name').html(client_name);
        socket = io.connect(socketIOurl, { query: "access_token=" + token + "&room=" + room + "&client_id=" + client_id + "&display_name=" + client_name });
        socket.on('child_added', function (data) {
            var message = data.message;
            var sender_name = data.owner;
            var clientId = data.clientId;
            console.log('child_added', data);
            // $("#usr"+clientId)[0].click();
            //wael recieved massege
            openChat(clientId);
            $("#" + clientId).chatbox("option", "boxManager").addMsg(sender_name, message);
            newMsgSound.play();
            $('#' + clientId).chatbox('option', "boxManager").highlightBox();
            //Local Notification
            if (windowFocus == false) {
                showNotification(sender_name + ': ' + message, 'img/chat.png', sender_name + ' sent New Message');
            }
        });

        socket.on('user_update', function (data) {
            console.log('user_update', data);
            if (data.client_id == client_id) {
                return;
            }
            var scope = angular.element($("#chatUsers")).scope();
            scope.$apply(function () {
                scope.users[data.client_id] =
                {
                    chat_id: data.client_id,
                    socket_id: data.client_socket_id,
                    name: data.display_name,
                    status: data.status,
                    pic: "",
                    position: "Employee"
                };
                console.log('new', scope.users[data.client_id]);
                // scope.users.push(newUserData);
                // }
            });
        });

        socket.on('disconnect_user', function (data) {
            console.log('dis', data);
            var scope = angular.element($("#chatUsers")).scope();
            scope.$apply(function () {
                for (var i in scope.users) {
                    if (scope.users[i].socket_id == data.client_socket_id) {
                        scope.users[i].status = "incognito";
                        // jQuery('#'+scope.users[i].chat_id).chatbox("option","boxManager").addMsg('','Goes Offline');
                        break;
                    }
                }
            });
        });

        $("a[data-chat-id]:not(.incognito)").on('click', function (a, b) {
            console.log('click');
            var e = $(this);

            openChat(e.attr('data-chat-id'));
            a.preventDefault()
        });
    });

    function openChat(divId) {
       alert('ssssssss');
        var c = $('#usr' + divId),
        d = c.attr("data-chat-id"),
        e = c.attr("data-chat-fname"),
        f = c.attr("data-chat-lname"),
        g = c.attr("data-chat-status") || "online",
        h = c.attr("data-chat-alertmsg"),
        i = c.attr("data-chat-alertshow") || !1;
        chatboxManager.addBox(d, {
            "title": "username" + d, "first_name": e, "last_name": f, "status": g, "alertmsg": h, "alertshow": i
        , messageSent: function (id, user, msg) {
            // alert(user.first_name+": "+ msg);
            // this.boxManager.addMsg(user.first_name, msg);
        }
        })
    }

} //EOF strtChat


var chatApp = app; //angular.module('ApplicationModule', []);

chatApp.filter('custom', function () {
    return function (input, search) {
        if (!input) return input;
        if (!search) return input;
        var expected = ('' + search).toLowerCase();
        var result = {};
        angular.forEach(input, function (value, key) {

            var actual = ('' + value.name).toLowerCase();
            if (actual.indexOf(expected) !== -1) {
                result[key] = value;
            }
        });
        return result;
    }
});



chatApp.controller('chatUsersController', chatUsersController);

function chatUsersController($scope) {
    $scope.users = {
        // {
        // 	chat_id:"cha1",
        // 	name: "Mohamed Hisham",
        // 	status: "online",
        // 	pic: "",
        // 	position: "Manager"
        // },
        // {
        // 	chat_id:"cha2",
        // 	name: "Ahmed Raffat",
        // 	status: "busy",
        // 	pic: "",
        // 	position: "Employee"
        // },
        // {
        // 	chat_id:"cha3",
        // 	name: "Hussam Hassan",
        // 	status: "incognito",
        // 	pic: "",
        // 	position: "Employee"
        // }
    };

    $scope.openchat = function (event, user) {
        var c = event.currentTarget || event.srcElement,
        d = c.attributes["data-chat-id"].value,
        e = c.attributes["data-chat-fname"].value,
        f = c.attributes["data-chat-lname"].value,
        g = c.attributes["data-chat-status"].value || "online",
        h = c.attributes["data-chat-alertmsg"].value,
        i = c.attributes["data-chat-alertshow"].value || !1;
        chatboxManager.addBox(d, {
            "title": "username" + d, "first_name": e, "last_name": f, "status": g, "alertmsg": h, "alertshow": i
        , messageSent: function (id, user, msg) {
            // alert(user.first_name+": "+ msg);
            // this.boxManager.addMsg(user.first_name, msg);
        }
        })
    };


};
