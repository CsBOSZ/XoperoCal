import { defineStore } from "pinia";

export const useUserStore = defineStore("user", {
  state: () => ({
    jwt: null,
    id: null,
    email: null,
    name: null,
    stringNotificationG: null,
    showNotificationG: false,
    ht: "http://localhost:5240",
  }),
  getters: {},
  actions: {
    login(EMAIL, PASSWORD) {
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      myHeaders.append("Accept", "text/plain");

      var raw = JSON.stringify({
        email: EMAIL,
        password: PASSWORD,
      });

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };

      fetch(this.ht + "/Auth/login", requestOptions)
        .then((response) => {
          if (Math.floor(+response.status / 100) != 2) {
            this.showNotificationG = true;
          } else {
            this.showNotificationG = false;
          }
          return response.text();
        })
        .then((result) => {
          this.stringNotificationG = result;

          if (!this.showNotificationG) {
            this.jwt = result;
            var myHeaders2 = new Headers();
            myHeaders2.append("Content-Type", "application/json");
            myHeaders2.append("Accept", "text/plain");
            myHeaders2.append("Authorization", "Bearer " + result);

            var requestOptions = {
              method: "GET",
              headers: myHeaders2,
              redirect: "follow",
            };

            fetch(this.ht + "/User/false/my", requestOptions)
              .then((response) => {
                if (Math.floor(+response.status / 100) != 2) {
                  this.showNotificationG = true;
                }
                return response.json();
              })
              .then((result) => {
                this.stringNotificationG = result;
                this.id = +result.id;
                this.email = result.email;
                this.name = result.name;
              })
              .catch((error) => console.log("error", error));
          }
        })
        .catch((error) => console.log("error", error));
    },
  },
});
