
let WindowOfPicture = document.getElementById("PictureWindow1");
let filterzSelector = document.getElementById("selectorOfFilterz1");

if(typeof(window.FileReader) == 'undefined'){
	WindowOfPicture.innerText('Не поддерживается браузером ' + navigator.userAgent);	
}

WindowOfPicture.addEventListener("ondragenter", () => PreventDefault(event));
WindowOfPicture.addEventListener("dragover", () => DragOverFileEventProcess('rgb(255, 0, 0)'));
WindowOfPicture.addEventListener("dragleave", () => DragOverFileEventProcess('rgb(255, 255, 255)'));
WindowOfPicture.addEventListener("drop", () => DropEventProcess(event));

document.getElementById("sendPicture1Button").addEventListener("click", () => SendRequest());

filterzSelector.addEventListener("change", ShowHint);

document.getElementById("SelectFilterButton1").addEventListener("click", AddSelectedFilter);
document.getElementById("ClearAllSelectedFilterzButton").addEventListener("click", ClearSelectedFilterzList);

document.getElementById("selectorOfFilterz1").addEventListener("dblclick", AddSelectedFilter);


function PreventDefault(event){
	event.preventDefault();
}

function DragOverFileEventProcess(Color){
	event.preventDefault();
	WindowOfPicture = document.getElementById("PictureWindow1");
	WindowOfPicture.style.background = Color;
}

function DropEventProcess(event){
	event.preventDefault();	
	let pictureSourceContainer = document.getElementById("picture1OfWindow1");
	
	RemoveByTagName('H2');
	
	let reader = new FileReader();
		  
			  // reader.onload = function(refToReader){				  
				  // pictureContainer.src = refToReader.target.result;				  
			  // };
			  
	reader.onload = (refToReader) => pictureSourceContainer.src = refToReader.target.result;				  		  
	reader.readAsDataURL(event.dataTransfer.files[0]);
}



function RemoveByTagName(tagName){
	let elementToRemoveN = WindowOfPicture.querySelectorAll(tagName);
	for(let elementToRemove of elementToRemoveN)
		elementToRemove.remove();

}




function SendRequest(){
	
	let pictureInputContainer = document.getElementById("picture1OfWindow1").src;
	
	if(pictureInputContainer == "http://localhost:8888/OnlineEditor.html") {
	alert("Изображение не выбрано");
	return;
	}
	
	
	let Request = new XMLHttpRequest();
	
	let pictureResultContainer = document.getElementById("picture1OfWindow2");
	
	Request.open("POST", "/PreprocessImage");
	
	let formDataToSend = MakeFormData(pictureInputContainer);
	
	if(formDataToSend == null){ return; }
	
	Request.send(formDataToSend);
	
	Request.onload = () => {
		if(Request.status != 200){ alert('Ошибка на стороне сервера')}
		else{
		
		pictureResultContainer.src = Request.response;		
		console.log(Request.response);
		}		
	}
		
}


function MakeFormData(refToPictureSrc){
	let toReturn = new FormData();	
	
	toReturn.append("ImageToProcess", refToPictureSrc);
	toReturn.append("commandzToPreprocessor", MakeCommandzList());
	
	let scopezList = MakeScopezList();
	if(scopezList == null) {return null; }
	
	toReturn.append("scopezValuezToPreprocessor", scopezList);
	
	return toReturn;
}




function ShowHint(){
	let hintWindow = document.getElementById("FilterzHintWindow1");
	
	if(filterzSelector.selectedIndex == 0) hintWindow.value = "Округляет все спекты RGB к ближайшему из значений 0, 128 или 255";
	else if(filterzSelector.selectedIndex == 1) hintWindow.value = "Окрашивает все серые цвета в красный";
	else if(filterzSelector.selectedIndex == 2) hintWindow.value = "Усиливает все цвета сохраняя пропорции";
	else if(filterzSelector.selectedIndex == 3) hintWindow.value = "Округляет серый цвет к ближайшему из значений 0, 128 или 255";
	else if(filterzSelector.selectedIndex == 4) hintWindow.value = "Уничтожает самый слабый цвет";
	else if(filterzSelector.selectedIndex == 5) hintWindow.value = "Оставляет только самый сильный цвет спектра";
	else if(filterzSelector.selectedIndex == 6) hintWindow.value = "Усредняет серый пиксель окружённый другими серыми пикселями";
}


function ClearSelectedFilterzList(){
	let filterzList = document.getElementById("SelectedFilterzList");
	filterzList.value = "";
}

function AddSelectedFilter(){	
	let SelectedFilterzList = document.getElementById("SelectedFilterzList");	
	SelectedFilterzList.value += (filterzSelector.options[filterzSelector.selectedIndex].innerHTML + "\n");	
}



function MakeCommandzList(){
	
	let SelectedFilterzList = document.getElementById("SelectedFilterzList").value;
	
	let filterN = SelectedFilterzList.split("\n");
	
	let toReturn = [];
	
	for(let i = 0; i < filterN.length; i++){				
		for(let j = 0; j < filterzSelector.options.length; j++){
			
			if(filterN[i] == filterzSelector.options[j].innerText){
				toReturn.push(filterzSelector.options[j].value);
			}
		
		}
	}
	
	return toReturn;
}


function MakeScopezList(){
		
	let toReturn = [];
	
	let lowerScope = document.getElementById("lowerScopeTextBox").value;
	let upperScope = document.getElementById("upperScopeTextBox").value;
	
	if(isNaN(lowerScope) || isNaN(upperScope)) 
	{
		alert('В полях указаны нечисловые значения'); 
		return null;
	}
	
	toReturn.push(document.getElementById("lowerScopeTextBox").value);
	toReturn.push(document.getElementById("upperScopeTextBox").value);
	
	return toReturn;
}