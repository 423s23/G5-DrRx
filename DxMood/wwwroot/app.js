const serviceURL = "http://localhost:5191";
const diagnoseButton = document.getElementById("diagnose")

const getPatients = (username, password) => {
    axios
        .get(`${serviceURL}/doctor/${username}/${password}`)
        .then((response) => {
            console.log(`GET patients`, response);
        })
        .catch((error) => console.error(error));
};

const createPatient = (id) => {
    axios
        .put(`${serviceURL}/patient/${id}`)
        .then((response) => {
            console.log(`PUT patient`, response);
        })
        .catch((error) => console.error(error));
};

diagnoseButton.onclick(()=>{
    createPatient();
})

//getPatients();
