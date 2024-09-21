import { EventDto, uploadEvent } from "@/services/apis/events/uploadEvent";
import { useMutation } from "@tanstack/react-query";

export interface UploadEventParams {
    event: EventDto;
    image: File;
}

export function useUploadEvent() {
    return useMutation({
        mutationFn: (params: UploadEventParams) => uploadEvent(params.event, params.image),
    })
}