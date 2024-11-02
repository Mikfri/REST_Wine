// use https (http secure).
// http (non secure) will make the app complain about mixed content when running the app from Azure
const baseUrl = "https://winerest.azurewebsites.net/api/Wine"

Vue.createApp({
    data() {
        return {
            objList: [],
            singleObj: null,
            filterInputValue: "",
            deleteIdInputValue: "",
            getIdInputValue: "",
            addData: { manufacturer: "", year: "", price: "", rating: ""},
            updData: { id: "", manufacturer: "", year: "", price: "", rating: ""},
            getMessage: "",
            addMessage: "",
            updateMessage: "",
            deleteMessage: "",
        }
    },
    async created() {
        this.getAllObjs(baseUrl);
    },
    methods: {
        //---: CRUD SEKTION :----
        async getAllObjs() {
            try {
                const response = await axios.get(baseUrl);
                this.objList = response.data;
            } catch (ex) {
                alert(ex.message);
            }
        },       
        async getById(id) {
            const url = baseUrl + "/" + id
            try {
                const response = await axios.get(url)
                this.singleObj = await response.data
                this.getMessage = `${response.status} ${response.statusText}`
            } catch (ex) {
                this.getMessage = ex.response.data
                //alert(ex.message)
            }
        },
        async addObj() {
            try {
                response = await axios.post(baseUrl, this.addData)
                this.addMessage = `${response.status} ${response.statusText}`
                this.getAllObjs()
            } catch (ex) {
                this.addMessage = ex.response.data
                //alert(ex.message)
            }
        },
        async updateObj() {
            const url = baseUrl + "/" + this.updData.id
            try {
                response = await axios.put(url, this.updData)
                this.updateMessage = `${response.status} ${response.statusText}`
                this.getAllObjs()
            } catch (ex) {
                this.updateMessage = ex.response.data
                //alert(ex.message)
            }
        },
        async deleteObj(deleteIdInput) {
            const url = baseUrl + "/" + deleteIdInput
            try {
                response = await axios.delete(url)
                this.deleteMessage = `${response.status} ${response.statusText}`
                this.getAllObjs()
            } catch (ex) {
                this.deleteMessage = ex.response.data
                console.error
                //alert(ex.message)
            }
        },
        //---: FILTER & SORTING SECTION :---
        filterByManufacturer(list) {
            if (!this.filterInputValue) {
                return list;
            }
            return list.filter(obj => obj.manufacturer.toLowerCase().includes(this.filterInputValue.toLowerCase()));
        },
        sortById() {
            this.objList.sort((obj1, obj2) => obj1.id - obj2.id)
        },
        sortByIdDesc() {
            this.objList.sort((obj1, obj2) => obj2.id - obj1.id)
        },
        sortByManufacturer() {
            this.objList.sort((obj1, obj2) => obj1.manufacturer.localeCompare(obj2.manufacturer))
        },
        sortByManufacturerDesc() {
            this.objList.sort((obj1, obj2) => obj2.manufacturer.localeCompare(obj1.manufacturer))
        },
        sortByYear() {
            this.objList.sort((obj1, obj2) => obj1.year - obj2.year)
        },
        sortByYearDesc() {
            this.objList.sort((obj1, obj2) => obj2.year - obj1.year)
        },
        sortByPrice() {
            this.objList.sort((obj1, obj2) => obj1.price - obj2.price)
        },
        sortByPriceDesc() {
            this.objList.sort((obj1, obj2) => obj2.price - obj1.price)
        },
        sortByRatingAscending() {
            this.objList.sort((obj1, obj2) => obj1.rating - obj2.rating)
        },
        sortByRatingDescending() {
            this.objList.sort((obj1, obj2) => obj2.rating - obj1.rating)
        },
    }
}).mount("#app")