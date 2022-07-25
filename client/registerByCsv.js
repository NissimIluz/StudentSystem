var selected = [];
var studentsArray = [];
function uplaud() {
    var file = input.files[0];
    if(!file) {
        showMassage(false,"בחר תחילה קובץ");
        return;
    }
    let formData = new FormData();    
    formData.append("file", file);

    var resultPromise = postFile('Students/RegisterByCsv',formData);
    resultPromise.then(result => {
        if(!result)
        
            showMassage(false,"שגיאה בהעלאת הקובץ ")
        else {
            studentsArray = result;
            students.innerHTML  = "";
            selectedButton.innerHTML = "";
            selectedStudents.innerHTML="";
            debugger
            if(result.length > 0) {
                students.innerHTML  = "<div >כפילויות: </div>";
                selectedStudents.innerHTML="נבחרו:";
                selectedButton.innerHTML  += '<div ><button onclick="confirm()">סיימתי לבחור</button> </div>';
            
                result.forEach(x => {
                    students.innerHTML  += getStudentRow(x);
                });
            } 
        }
    });
    return false;
}
function getStudentRow(data) {
    return `<div class="student-row">${data.id} ${data.firstName} ${data.lastName}  <button onclick="select(${data.id})">בחר</button> </div>`
}
function select(id){
    debugger
    if (!selected.includes(id)) {
        selectedStudents.innerHTML += `<div class="student-row">${id} </div>`
        selected.push(id);
    }
}
function confirm() {
    var data = studentsArray.filter(s => selected.includes(s.id));
    post('Students/RegisterMany',data).then( result  => {
        if(result===true) {
            showMassage(true, "בוצע");
        }
        else {
            showMassage(false, "נכשל");
        }
    })
}
function showMassage(status, massage){
    resultMassageContainer.style.display = "flex";
    resultMassage.textContent  = massage;
    if(status===true) {
        resultMassageContainer.classList.add("success");
    }
    else {
        resultMassageContainer.classList.add("failed");
    }
    resultMassageContainer.classList.re
    setTimeout(()=> {
        resultMassage.textContent ='';
        resultMassageContainer.style.display = "none";
        resultMassageContainer.classList.remove("failed");
        resultMassageContainer.classList.remove("success");
        }
        , 15000);
}