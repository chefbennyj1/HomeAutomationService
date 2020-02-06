(function () {
    var ApiClient = function (url, endpoint) {
        return new Promise((resolve, reject) => {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', url + endpoint, true);
            xhr.setRequestHeader('Content-Type', 'application/json');
            xhr.withCredentials = true;
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        resolve(JSON.parse(xhr.responseText));
                    } else {
                        reject(xhr.status);
                    }
                }
            };
            xhr.send();
        });

    } 

    function getDeviceStateIconInfo(device) {
        switch (device.category) {
            case 0:
                return {
                    name: "Plugin",
                    icons: {
                        icon_on  : { name : "mdi-puzzle", state: "stateOff" },
                        icon_off : { name : "mdi-puzzle", state: "stateOff" }
                    }
                };
            case 1:
                return {
                    name: "Interface",
                    icons: {
                        icon_on  : { name : "mdi-puzzle", state: "" },
                        icon_off : { name : "mdi-puzzle", state: "" }
                    }
                };
            case 2:
                return {
                    name: "Dimmable Switch",
                    icons: {
                        icon_on  : { name : "mdi-lightbulb-on-outline", state: "stateOn" },
                        icon_off : { name : "mdi-lightbulb",state: "stateOff" }
                    }
                };

            case 3:
                switch (device.name.indexOf('Fireplace') > -1) {
                    case true:
                        return {
                            name: "Electric Fireplace",
                            icons: {
                                icon_on  : { name : "mdi-fire", color: "stateOn" },
                                icon_off : { name : "mdi-fire", color: "stateOff" }
                            }
                        };
                    default:
                        return {
                            name: "On/Off Switch",
                            icons: {
                                icon_on  : { name : "mdi-lightbulb-on-outline", state: "stateOn" },
                                icon_off : { name : "mdi-lightbulb", state: "stateOff" }
                            }
                        }
                };

            case 4: //Sensors
                switch (device.subcategory) {
                    case 1:  //Door Sensor
                        return {
                            name: "Door Sensor",
                            icons: {
                                icon_on  : { name : "mdi-door-open", state: "stateHot" },
                                icon_off : { name : "mdi-door-closed", state: "stateOff" }
                            }
                        };
                    case 2:  //Leak Sensor
                        return {
                            name: "Leak Sensor",
                            icons: {
                                icon_on  : { name : "mdi-close-circle", state: "stateHot" },
                                icon_off : { name : "mdi-checkbox-marked-circle", state: "stateOff" }
                            }
                        };
                    case 3:  //Motion Sensor
                        return {
                            name: "Motion Sensor",
                            icons: {
                                icon_on  : { name : "mdi-walk", state: "" },
                                icon_off : { name : "mdi-checkbox-marked-circle", state: "stateOff" }
                            }
                        };
                    case 4:  //Smoke Sensor
                        return {
                            name: "Smoke Sensor",
                            icons: {
                                icon_on  : { name : "mdi-walk", state: "stateHot" },
                                icon_off : { name : "mdi-smoke-detector", state: "stateOff" }
                            }
                        };
                    case 5:  //CO Sensor
                        return {
                            name: "CO Sensor",
                            icons: {
                                icon_on  : { name : "mdi-alert", state: "stateHot" },
                                icon_off : { name : "mdi-smoke-detector", state: "stateOff" }
                            }
                        };
                    case 6:   // Glass Break Sensor
                        return {
                            name: "CO Sensor",
                            icons: {
                                icon_on  : { name : "mdi-alert", state: "stateHot" },
                                icon_off : { name : "mdi-smoke-detector", state: "stateOff" }
                            }
                        };
                    case 7:    //Freeze Sensor
                    case 8:   //Binary Sensor
                }
                return { name: "Sensor", icon_on: "mdi-close-circle", icon_off: "mdi-checkbox-marked-circle" };
            case 5:  //HVAC
                //mdi-fan
                switch (device.subcategory) {
                    case 1:
                        switch (device.mode) {
                            case "HeatOn":
                                return {
                                    name: "HVAC",
                                    icons: {
                                        icon_on  : { name: "mdi-thermostat", state: "stateHot" },
                                        icon_off : { name: "mdi-thermostat", state: "stateOff" }
                                    }
                                };
                            
                            case "Off":
                                return {
                                    name: "HVAC",
                                    icons: {
                                        icon_on  : { name: "mdi-thermostat", state: "stateOff" },
                                        icon_off : { name: "mdi-thermostat", state: "stateOff" }
                                    }
                                };
                            
                            case "CoolOn":
                                return {
                                    name: "HVAC",
                                    icons: {
                                        icon_on  : { name : "mdi-thermostat", state: "stateCold" },
                                        icon_off : { name : "mdi-thermostat", state: "stateOff"  }
                                    }
                                };
                            
                        case "AutoChangeOver":
                            return {
                                name: "HVAC",
                                icons: {
                                    icon_on  : { name : "mdi-thermostat", state: "stateOff" },
                                    icon_off : { name : "mdi-thermostat", state: "stateOff"
                                    }
                                }
                            };
                        default:
                            return {
                                name: "HVAC",
                                icons: {
                                    icon_on  : { name : "mdi-thermostat", state: "stateOff" },
                                    icon_off : { name : "mdi-thermostat", state: "stateOff" }
                                }
                            };
                        }
                        
                }


            case 6: //Camera
                switch (device.commFailure) {
                    case "0":
                        return {
                            name: "Camera",
                            icons: {
                                icon_on: { name: "mdi-camcorder", state: "stateCold" },
                                icon_off: { name: "mdi-camcorder", state: "stateCold" }
                            }
                        };
                    case "1":
                        return {
                            name: "Camera",
                            icons: {
                                icon_on: { name: "mdi-camcorder-off", state: "stateError" },
                                icon_off: { name: "mdi-camcorder-off", state: "stateError" }
                            }
                        };
                }
                return {
                    name: "Camera",
                    icons: {
                        icon_on  : { name : "mdi-camcorder", state: "stateCold" },
                        icon_off : { name : "mdi-camcorder-off", state: "stateOff"
                        }
                    }
                };

            case 7: //Door Lock
                //mdi-lock-smart
                //mdi-lock-open
                //mdi-lock
                return {
                    name: "Door Lock",
                    icons: {
                        icon_on  : { name : "mdi-lock", state: "stateLocked"  },
                        icon_off : { name : "mdi-lock-open", state: "stateUnlocked" }
                    }
                };



            case 8:
                return {
                    name: "Window Covering",
                    icons: {
                        icon_on  : { name : "mdi-arrow-expand-vertical", state: "stateHot" },
                        icon_off : { name : "mdi-blinds", state: "stateOff" }
                    }
                };

            case 9:
                return { name: "Remote Control", icon_on: "", icon_off: "" };
            case 10:
                return { name: "IR Transmitter", icon_on: "", icon_off: "" };
            case 11:
                return { name: "Generic I/O", icon_on: "", icon_off: "" };
            case 12:
                return { name: "Generic Sensor", icon_on: "", icon_off: "" };
            case 13:
                return { name: "Serial Port", icon_on: "", icon_off: "" };
            case 14:
                return {
                    name: "Scene Controller",
                    icons: {
                        icon_on  : { name : "mdi-trackpad", state: "stateCold" },
                        icon_off : { name : "mdi-trackpad", state: "stateHot" }
                    }
                };

            case 15:
                return { name: "A/V", icon_on: "", icon_off: "" };
            case 17:
                return { name: "Temperature Sensor", icon_on: "mdi-thermometer-lines", icon_off: "" };
            case 18:
                return { name: "Light Sensor", icon_on: "", icon_off: "" };
            case 19:
                return { name: "Z-Wave Interface", icon_on: "", icon_off: "" };
            case 20:
                return { name: "Insteon Interface", icon_on: "", icon_off: "" };
            case 21:
                return { name: "Power Meter", icon_on: "", icon_off: "" };
            case 22:
                return { name: "Alarm Panel", icon_on: "", icon_off: "" };
            case 23:
                return { name: "Alarm Partition", icon_on: "", icon_off: "" };
            case 24:
                return { name: "Siren", icon_on: "mdi-security-home", icon_off: "mdi-security-close" };
        }
        //mdi-home-alert
        //mdi-temperature-celsius
        //mdi-temperature-fahrenheit
    }
    
    var homeContainer          = document.querySelector('.homeContainer');
    var roomSelectionContainer = document.querySelector('.roomSelectionContainer');
    
    function getVeraInfo() {
        return new Promise((resolve, reject) => {
            ApiClient("http://192.168.2.104:9925/vera/values/3", "").then((result) => {
                resolve(JSON.parse(result));
            });
        });
    }

    pageLoad();

    function pageLoad() {
        getVeraInfo().then((data) => {
            console.log(data.rooms);
            console.log(data.devices);

            var roomContainerHtml = '';
            var roomSelectionHtml = '';
            data.rooms.forEach((room) => {

                roomContainerHtml += getRoomContainerHtml(room);
                
                roomSelectionHtml += getRoomSelectionButtonHtml(room);

            });

            roomSelectionContainer.innerHTML = roomSelectionHtml;
            createRoomSelectionGlider(roomSelectionContainer);
             
            homeContainer.innerHTML          = roomContainerHtml;
            //#\34  > div 

            data.devices.forEach((device) => {
                var roomContainer = document.querySelector('[data-room="' + device.room + '"]'); // ('#' + CSS.escape(device.room));
                roomContainer.innerHTML += getDeviceHtml(device);
                
            });

            document.querySelectorAll('.glider.deviceCardGlider').forEach((element) => {
                createDeviceCardGlider(element);
            });
            
            updateDeviceState();

        });
    }

    function updateDeviceState() {
        getVeraInfo().then((data) => {
            data.devices.forEach((device) => {

                var deviceContainer = document.querySelector('[data-id="' + device.id + '"]');
                var iconElement = deviceContainer.querySelector('i');
                var deviceInfo = getDeviceStateIconInfo(device);

                var icon = (device.status == 1 || device.locked == 1 || device.category == 5
                    ? deviceInfo.icons.icon_on
                    : deviceInfo.icons.icon_off);

                //Change Icon state color
                if (!iconElement.classList.contains(icon.state)) {
                    iconElement.classList.remove(deviceInfo.icons.icon_on.state == icon.state
                        ? deviceInfo.icons.icon_off.state
                        : deviceInfo.icons.icon_on.state);
                    iconElement.classList.add(icon.state);
                }
                //Change icon
                if (!iconElement.classList.contains(icon.name)) {
                    iconElement.classList.remove(deviceInfo.icons.icon_on.name == icon.name
                        ? deviceInfo.icons.icon_off.name
                        : deviceInfo.icons.icon_on.name);
                    iconElement.classList.add(icon.name);
                }

                if (device.category == 5) {
                    var mode = deviceContainer.querySelector('.thermostatMode');
                    var html = '';
                    switch (device.hvacstate) {
                    case "FanOnly":
                        html += '<i class="mdi mdi-48px mdi-fan stateOff mdi-spin"></i>';
                        break;
                    case "Heating":
                        html += '<i class="mdi mdi-48px mdi-waves stateHot"></i>';
                        break;
                    case "Cooling":
                        html += '<i class="mdi mdi-48px mdi-snowflake stateCold"></i>';
                        break;
                    case "Idle":
                        html += '<i class="mdi mdi-48px mdi-pause stateOff"></i>';
                        break;
                    }
                    mode.innerHTML = html;
                } 
                
            });
            setTimeout(() => { updateDeviceState(); }, 2000);
        });
    }

    function createRoomSelectionGlider(element) {
        return new Glider((element),
            {
                slidesToShow: '2',
                duration     : 0.2,
                scrollLock   : true,
                draggable    : true,
                dots         : '#dotsRooms',
                exactWidth   : false,
                arrows       : { prev : '.room-prev', next : '.room-next' }
            });
    }


    function createDeviceCardGlider(element) {
        return new Glider((element),
            {
                slidesToShow : 1,
                duration     : 0.2,
                scrollLock   : true,
                itemWidth    : document.querySelector('.deviceCard').style.width,
                draggable    : true,
                dots         : '#dots' +  element.dataset.room,
                exactWidth   : false,
                arrows       : { prev: '.device-prev-' + element.dataset.room, next: '.device-next-' + element.dataset.room }
            });
    }

    function getRoomContainerHtml(room) {
        var html = '';
        html += '<div id="' + room.id + '" class="room">';
        html += '<p>' + room.name + '</p>';
        html += '<div class="glider-contain multiple">';
        html += '<div data-room="' + room.id + '" class="glider deviceCardGlider">';
        html += '</div>';
        html += getDeviceCardGliderButtonsHtml(room.id);
        html += '</div>';
        html += '</div>';
        return html;
    }
         //onclick="smoothScroll(document.querySelector("[data-room=\'' + room.id + '\']"))"
    function getRoomSelectionButtonHtml(room) {
        var html = '';
        html += '<div class="cardContainer">';
        html += '<div class="buttonContainer roomButtonContainer">';
        html += '<a onclick="showRoom(' + room.id + ')" class="cta">';
        html += '<p>' + room.name + '</p>';
        html += '</a>';
        html += '</div>';
        html += '</div>';
        return html;
    }

    function getDeviceHtml(device) {
        var html = '',
            icon,
            deviceIconInfo;
        
        switch (device.category) {
            case 5:

                html += '<div data-id="' + device.id + '" class="cardContainer">';
                html += '<div class="deviceCard">';
                html += '<div  class="buttonContainer">';
                html += '<a href="#" class="cta">';

                deviceIconInfo = getDeviceStateIconInfo(device);

                icon = deviceIconInfo.icons.icon_on;

                html += '<i class="mdi mdi-48px ' + icon.name + ' ' + icon.state + '"></i>';

                html += '</a>';
                html += '</div>';

                html += '<div class="thermostatContainer">';
                html += '<p>' + device.name + '</p>';
                html += '<h1 class="stateOff">' + device.temperature + '<i class="mdi mdi-48px mdi-temperature-celsius stateOff"></i></h1>';
                html += '<h3 class="stateOff">set point: ' + device.setpoint + '<i class="mdi mdi-temperature-celsius stateOff"></i></h3>';
                html += '</div>';
                html += '<div class="thermostatMode">';
                switch (device.hvacstate) {
                    case "FanOnly":
                        html += '<i class="mdi mdi-48px mdi-fan stateOff mdi-spin"></i>';
                        break;
                    case "Heating":
                        html += '<i class="mdi mdi-48px mdi-waves stateHot"></i>';
                        break;
                    case "Cooling":
                        html += '<i class="mdi mdi-48px mdi-snowflake stateCold"></i>';
                        break;
                    case "Idle":
                        html += '<i class="mdi mdi-48px mdi-pause stateOff"></i>';
                        break;
                }
                html += '</div>';

                html += '</div>';
                html += '</div>';
                return html;
            default:
                html += '<div data-id="' + device.id + '" class="cardContainer">';
                html += '<div class="deviceCard">';
                html += '<div  class="buttonContainer">';
                html += '<a href="#" class="cta">';

                deviceIconInfo = getDeviceStateIconInfo(device);

                icon = (device.status == 1 || device.locked == 1 ? deviceIconInfo.icons.icon_on : deviceIconInfo.icons.icon_off);

                html += '<i class="mdi mdi-48px ' + icon.name + ' ' + icon.state + '"></i>';
                html += '</a>';
                html += '</div>';
                html += '<p>' + device.name + '</p>';
                return html;
        }

    }

    function getDeviceCardGliderButtonsHtml(id) {
        var html = '';
        html += '<button class="glider-prev device-prev-' + id + '"><i class="mdi mdi-48px mdi-chevron-left"></i></button>';
        html += '<button class="glider-next device-next-' + id + '"><i class="mdi mdi-48px mdi-chevron-right"></i></button >';
        html += '<div role="tablist" id="dots' + id + '" class="dots"></div>';
        return html;
    }
     
})();
function showRoom(id) {
    var element = document.querySelector('#' + CSS.escape(id));
    var scrollY = element.offsetTop -
        (document.querySelector('.headerContainer').offsetHeight + 5);
    document.documentElement.scrollTo(0, scrollY);
    
}