import { defineStore } from "pinia";

export const useUserStore = defineStore("user", {
  state: () => ({
    jwt: null,
    id: null,
    email: null,
    name: null,
    color: null,
    stringNotificationG: null,
    showNotificationG: false,
    ht: "http://localhost:5240",
    se: 0,
    ss: 0
  }),
  getters: {},
  actions: {
    async login(EMAIL, PASSWORD) {
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

      await fetch(this.ht + "/Auth/login", requestOptions)
        .then((response) => {
          this.showNotificationG = (Math.floor(+response.status / 100) != 2) 
          return response.text();
        })
        .then((result) => {
          this.stringNotificationG = result;
         }
        )
        .catch((error) => console.log("error", error));

         

        if (!this.showNotificationG) {
            this.jwt = this.stringNotificationG;
            var myHeaders2 = new Headers();
            myHeaders2.append("Content-Type", "application/json");
            myHeaders2.append("Accept", "text/plain");
            myHeaders2.append("Authorization", "Bearer " + this.jwt);

            var requestOptions = {
              method: "GET",
              headers: myHeaders2,
              redirect: "follow",
            };
        await fetch(this.ht + "/User/false/my", requestOptions)
              .then((response) => {
                this.showNotificationG = (Math.floor(+response.status / 100) != 2) 
                return response.json();
              })
              .then((result) => {
                this.stringNotificationG = result;
                this.id = +result.id;
                this.email = result.email;
                this.name = result.name;
                this.color = result.color;
              })
              .catch((error) => console.log("error", error));
          }

    },
  },
});
