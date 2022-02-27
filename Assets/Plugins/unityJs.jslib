mergeInto(LibraryManager.library, {

  PostResults: function (str) {
    window.PostResults(Pointer_stringify(str));
  },

  GetUser: function () {
    var string = window.parent.GetUser();
    var userId = string;
    var bufferSize = lengthBytesUTF8(userId) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(userId, buffer, bufferSize);
    return buffer;
  },

});