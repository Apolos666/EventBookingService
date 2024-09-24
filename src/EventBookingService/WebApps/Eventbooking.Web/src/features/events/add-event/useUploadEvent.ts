
import { EventDto } from "@/services/apis/events/types";
import { uploadEvent } from "@/services/apis/events/uploadEvent";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { homePageConfig } from "@/pages/home/HomePage.config";

export interface UploadEventParams {
    event: EventDto;
    image: File;
}

export function useUploadEvent() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (params: UploadEventParams) => uploadEvent(params.event, params.image),
        onSettled: async (_) => {
            await queryClient.invalidateQueries({ queryKey: ["events", homePageConfig.pagination.pageNumber, homePageConfig.pagination.pageSize] } );
        }
    })
}