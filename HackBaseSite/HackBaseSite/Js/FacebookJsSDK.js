window.fbLoggedIn = false;
window.fbAsyncInit = function () {
      // init the FB JS SDK
      FB.init({
          appId: '218192968345470',                        // App ID from the app dashboard
          channelUrl : 'http://localhost:49407/channel.html', // Channel file for x-domain comms
          status: false,                                  // Check Facebook Login status
          cookie: true,                                 // enable cookies to allow the server to access the session
          xfbml: true                                  // Look for social plugins on the page
      });

      // Additional initialization code such as adding Event Listeners goes here
      FB.getLoginStatus(function (response) {
          if (response.status === 'connected') {
              window.fbLoggedIn = true;
              var uid = response.authResponse.userID;
              FB.api('/me', function (response3) {
                  console.log('Good to see you, ' + response3.name + '.');
              });
              var accessToken = response.authResponse.accessToken;
          } else if (response.status === 'not_authorized') {
              window.fbLoggedIn = false;
              //Logged into facebook, but haven't 
              FB.login(function (response2) {
                  if (response2.authResponse) {
                      window.fbLoggedIn = true;
                      alert("Test1");
                      console.log('Welcome! Fetching your information...');
                      FB.api('/me', function (response3) {
                          console.log('Good to see you, ' + response3.name + '.');
                      });
                  } else {
                      console.log('User cancelled login or did not fully authorize.');
                  }
              });
          } else {
              window.fbLoggedIn = false;
              //Not logged in
          }

          if (window.fbLoggedIn)
            $(".fb-login-button").hide();
      });

      

  };

// Load the SDK asynchronously
(function(d, s, id){
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {return;}
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

