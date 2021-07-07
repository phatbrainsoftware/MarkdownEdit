export function insertText(id, text) {
    var element = document.getElementById(id);

    const [start, end] = [element.selectionStart, element.selectionEnd];

    element.setRangeText(text, start, end, 'end');

    return element.value;
}

export function wrapText(id, tag) {
    var element = document.getElementById(id);
    var start = element.selectionStart;
    var end = element.selectionEnd;

    if (element.value.substring(start, end).endsWith(" ")) {
        end = end - 1;
    }

    var text = element.value.substring(start, end);

    var replace = tag + text + tag;

    element.value = element.value.substring(0, start) + replace + element.value.substring(end, element.value.length);

    return element.value;
}

export function showPrompt(message) {
    return prompt(message, 'Type anything here');
}